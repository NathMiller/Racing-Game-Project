using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingObstacles : MonoBehaviour
{
    public float speed = 0.05f;
    public float xPos;
    public float zPos;

    

    void Start()
    {
     xPos = transform.position.x;
     zPos = transform.position.z;
    }
    void Update()
    {
     Vector3 pos1 = new Vector3(xPos, -5, zPos);
     Vector3 pos2 = new Vector3(xPos, 2, zPos);
   
    transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 3.0f));
        //yPos = transform.position.y;
        //if ((yPos < 11) && (yPos >= 10))
        //{
        //    Debug.Log("MADE IT");
        //    transform.Translate(Vector3.down * speed * Time.deltaTime);
        //}
        //else if (yPos <= 0) 
        //{
        //  transform.Translate(Vector3.up * speed * Time.deltaTime);
        //}
    }

}




    

    





