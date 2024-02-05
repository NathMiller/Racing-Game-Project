using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    [SerializeField]
    MeshCollider mesh;

    void Update()
    {
        Vector3 minBounds = mesh.bounds.min;
        Vector3 maxBounds = mesh.bounds.max;

        Debug.Log("min: " + minBounds + " max: " + maxBounds);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("COLLSSION FOUND");
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER FOUND");
            Physics.gravity = new Vector3(0, -20f, 0);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
    }
        
       
        


}
