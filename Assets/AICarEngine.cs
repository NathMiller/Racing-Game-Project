using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarEngine : MonoBehaviour
{
    private Rigidbody rigidBody;
    public Transform path;
    private List<Transform> nodes;
    private int currentNode = 0;
    public float maxSteerAngle = 30f;
    public float maxMotorTorque = 500f;
    public float maxBrakeTorque = 1000f;
    public float currentSpeed;
    public float maxSpeed = 30;
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;
    public Transform massCenter;
    public bool isBraking = false;
    RaycastHit hit;
    public float sensorLength = 3f;
    public float frontSensorPosition = 1.5f;
    public float sideSensorPosition = 0.5f;
    public float AngleSensor = 30;
    private bool avoiding = false;
    private float targetSteerAngle = 0;
    public float turnSpeed = 20f;
    public int difficulty;
    public float distanceToNode = 5f;




    void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = massCenter.localPosition;

        difficulty = Random.Range(1, 4);

        if (difficulty == 1)
        {
            maxMotorTorque = 350f;
            maxSpeed = 30;
            distanceToNode = 3f;

        } else if (difficulty == 2)
        {
            maxMotorTorque = 500f;
            maxSpeed = 40;
            distanceToNode = 5f;
        } else if (difficulty == 3)
        {
            maxMotorTorque = 750f;
            maxSpeed = 50;
            distanceToNode = 10f;
        }


    }


    public void FixedUpdate()
    {
        ApplySteer();
        Drive();
        WayFinder();
        Braking();
        Sensors();
        LerpToSteerAngle();
    }

    private void ApplySteer()
    {

        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        relativeVector = relativeVector / relativeVector.magnitude;

        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        targetSteerAngle = newSteer;

    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * frontLeft.radius * frontLeft.rpm * 60 / 1000;
        if ((currentSpeed < maxSpeed) && !isBraking)
        {
            frontLeft.motorTorque = maxMotorTorque;
            frontRight.motorTorque = maxMotorTorque;
        }
        else
        {
            frontLeft.motorTorque = 0;
            frontRight.motorTorque = 0;
        }



    }
//change distance to current node per difficulty of the AI 
    private void WayFinder()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < distanceToNode)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
                
            }
            else
            {
                currentNode = currentNode + 1;
                
                
            }
        }
    }

    private void Braking()
    {
        if (isBraking)
        {
            rearLeft.brakeTorque = maxBrakeTorque;
            rearRight.brakeTorque = maxBrakeTorque;
        }
        else
        {
            rearLeft.brakeTorque = 0;
            rearRight.brakeTorque = 0;
        }

    }

    private void Sensors()
    {
        Vector3 sensorStartPos = transform.position;
        sensorStartPos.z += frontSensorPosition;
        float avoidMultiplier = 0;
        avoiding = false;

        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
            }
        }

        sensorStartPos.x += sideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.4f;
                rigidBody.velocity = new Vector3(0, 0, 4);
            }
        }

        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(AngleSensor, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.2f;
            }
        }

        sensorStartPos.x -= 2 * sideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.4f;
            }
        }

        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-AngleSensor, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.2f;
            }
        }

        if (avoidMultiplier == 0)
        {
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoiding = true;
                    if (hit.normal.x < 0)
                    {
                        avoidMultiplier = -0.4f;
                    }
                    else
                    {
                        avoidMultiplier = 0.4f;
                    }
                }

            }
        }


        if (avoiding)
        {
            targetSteerAngle = maxSteerAngle * avoidMultiplier;
        }

    }
    private void LerpToSteerAngle()
    {
        frontLeft.steerAngle = Mathf.Lerp(frontLeft.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        frontRight.steerAngle = Mathf.Lerp(frontRight.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }
}
