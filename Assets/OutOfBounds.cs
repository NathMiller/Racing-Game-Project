using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour 
{ 


    
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("COLLSSION FOUND");
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER FOUND");
            collider.gameObject.transform.position = new Vector3(25, 101, 23); 
            collider.gameObject.transform.rotation = new Quaternion(0,0,0,0);

        }
    }
}
