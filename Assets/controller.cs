using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controller : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque = 2000;
    public float maxSteeringAngle;


    public Rigidbody rigidBody;
    public Transform massCenter;

    public bool collisionOccured = false;
    public bool AI = false;

    public int attempts = 1;
    public int score = 0;

    private GameplayManager gameplayManager;

    Vector3 originalPos;
    Quaternion originalAng;

    public bool massAvailable = true;
    public bool speedAvailable = true;
    public bool DMDSAvailable = true;
    public bool SUMUAvailable = true;

    public TMPro.TMP_Text resetLabel;



    //[Header("Sensors")]
    //RaycastHit hit;
    //public float sensorLength = 10f;
    //public float frontSensorPosition = 1.5f;
    //public float sideSensorPosition = 0.5f;
    //public float AngleSensor = 30;
    //private bool avoiding = false;
    //public WheelCollider leftWheel;
    //public WheelCollider rightWheel;




    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = massCenter.localPosition;
        originalPos = gameObject.transform.localPosition;
        originalAng = gameObject.transform.localRotation;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.mass = 1500;

        
    }


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        gameplayManager = GameObject.FindObjectOfType<GameplayManager>();
    }



    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {

        if ((gameplayManager.massUpgrade) && (massAvailable))
        {
            Debug.Log("mass upgraded");
            rigidBody.mass = 750;
            score = score - 300;
            massAvailable = false;
        }
        else if (massAvailable)
        {
            
            Debug.Log("mass not upgraded");
        }

        if (gameplayManager.speedUpgrade && (speedAvailable))
        {
            Debug.Log("speed upgraded");
            maxMotorTorque = 5000;
            score = score - 300;
            speedAvailable = false;

        }
        else if (speedAvailable)
        {
            
            Debug.Log("speed not upgraded");
        }

        if (gameplayManager.massDownSpeedDownUpgrade && (DMDSAvailable))
        {
            Debug.Log("dmds upgraded");
            maxMotorTorque = 1000;
            rigidBody.mass = rigidBody.mass - 500;
            score = score - 500;
            DMDSAvailable = false;
        }
        else if (DMDSAvailable)
        {
            Debug.Log("dmds not upgraded");
        }

        if (gameplayManager.speedUpMassUpUpgrade && (SUMUAvailable))
        {
            Debug.Log("sumu upgraded");
            maxMotorTorque = maxMotorTorque + 1500;
            rigidBody.mass = rigidBody.mass + 500;
            score = score - 500;
            SUMUAvailable = false;
        }
        else if (SUMUAvailable)
        {
            Debug.Log("SUMU not upgraded");
        }


        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        float speed = rigidBody.velocity.magnitude;

        if (Input.GetKeyDown("space") && (collisionOccured) && (attempts <= 2))
        {
            //car velocity will not return to zero unless car was stationary upon reset and i cannot fix that
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            gameObject.transform.position = originalPos;
            gameObject.transform.rotation = originalAng;
            rigidBody.drag = 0;
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            attempts = attempts + 1;
            collisionOccured = false;
            resetLabel.text = "";
            resetLabel.alignment = TMPro.TextAlignmentOptions.Center;

            gameplayManager.UpdateAttempts(attempts);
            

            Debug.Log("has reset");
        }

        if (Input.GetKeyDown("space") && (collisionOccured) && (attempts > 2))
        {
            Debug.Log("scene load");
            resetLabel.text = "";
            resetLabel.alignment = TMPro.TextAlignmentOptions.Center;

            rigidBody.drag = 0;
            SceneManager.LoadScene("race");
            
        }

       
        
            rigidBody.AddForce(transform.forward * motor);

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                
                
                 axleInfo.leftWheel.steerAngle = steering;
                 axleInfo.rightWheel.steerAngle = steering;
                
                    

                }
                if (axleInfo.motor)
                {

                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;

                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }


        

    }




    public void OnCollisionEnter(Collision collision)
    {
        Vector3 rv = collision.relativeVelocity;
        Vector3 j = collision.impulse;
        Vector3 n = collision.GetContact(0).normal;



        if (collisionOccured)
        {
            resetLabel.text = "Press space to reset";
            resetLabel.alignment = TMPro.TextAlignmentOptions.Center;

            Debug.Log("HERE: " + rv + " " + j + " " + n);
            
            gameplayManager.UpdateScore(score);
            return;
        }

        //if (collision.gameObject.CompareTag("Obstacle")) { rigidBody.velocity = new Vector3(0, 0, 0); }

        if (collision.gameObject.CompareTag("target100")) { score = score + 100; rigidBody.drag = 8; collisionOccured = true; }
        if (collision.gameObject.CompareTag("target200")) { score = score + 200; rigidBody.drag = 8; collisionOccured = true; }
        if (collision.gameObject.CompareTag("target300")) { score = score + 300; rigidBody.drag = 8; collisionOccured = true; }
        if (collision.gameObject.CompareTag("target400")) { score = score + 400; rigidBody.drag = 8; collisionOccured = true; }
        if (collision.gameObject.CompareTag("target500")) { score = score + 500; rigidBody.drag = 8; collisionOccured = true; }
        if (collision.gameObject.CompareTag("target1000")) { score = score + 1000; rigidBody.drag = 8; collisionOccured = true; }

        
    }

    public void AICar(bool AI)
    {
    //    if (AI)
    //    {
    //        rigidBody.velocity = new Vector3(0, 0, 4);

    //        Vector3 sensorStartPos = transform.position;
    //        sensorStartPos.z += frontSensorPosition;
    //        float avoidMultiplier = 0;
    //        avoiding = false;

    //        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
    //        {
    //            if (!hit.collider.CompareTag("Terrain"))
    //            {
    //                Debug.DrawLine(sensorStartPos, hit.point);
    //                avoiding = true;
    //            }

    //        }

    //        sensorStartPos.x += sideSensorPosition;
    //        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
    //        {
    //            if (!hit.collider.CompareTag("Terrain"))
    //            {
    //                Debug.DrawLine(sensorStartPos, hit.point);
    //                avoiding = true;
    //                avoidMultiplier -= 0.4f;
    //                rigidBody.velocity = new Vector3(0, 0, 4);
    //            }
    //        }

    //        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(AngleSensor, transform.up) * transform.forward, out hit, sensorLength))
    //        {
    //            if (!hit.collider.CompareTag("Terrain"))
    //            {
    //                Debug.DrawLine(sensorStartPos, hit.point);
    //                avoiding = true;
    //                avoidMultiplier -= 0.2f;
    //            }
    //        }

    //        sensorStartPos.x -= 2 * sideSensorPosition;
    //        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
    //        {
    //            if (!hit.collider.CompareTag("Terrain"))
    //            {
    //                Debug.DrawLine(sensorStartPos, hit.point);
    //                avoiding = true;
    //                avoidMultiplier += 0.4f;
    //            }
    //        }

    //        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-AngleSensor, transform.up) * transform.forward, out hit, sensorLength))
    //        {
    //            if (!hit.collider.CompareTag("Terrain"))
    //            {
    //                Debug.DrawLine(sensorStartPos, hit.point);
    //                avoiding = true;
    //                avoidMultiplier += 0.2f;
    //            }
    //        }

    //        if (avoidMultiplier == 0)
    //        {
    //            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
    //            {
    //                if (!hit.collider.CompareTag("Terrain"))
    //                {
    //                    Debug.DrawLine(sensorStartPos, hit.point);
    //                    avoiding = true;
    //                    if(hit.normal.x < 0)
    //                    {
    //                        avoidMultiplier = -0.4f;
    //                    } else
    //                    {
    //                        avoidMultiplier = 0.4f;
    //                    }
    //                }

    //            }
    //        }
            

    //        if (avoiding)
    //        {
                
    //                leftWheel.steerAngle = maxSteeringAngle * avoidMultiplier;
    //                rightWheel.steerAngle = maxSteeringAngle * avoidMultiplier;
              


    //        }

    //    }

    }
}



[System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }
    

