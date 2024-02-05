using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    Scene m_Scene;
    string sceneName;
    bool hasLoaded = false;
    GameObject player = GameObject.FindGameObjectWithTag("Player");


    void Start()
    {

    }
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Debug.Log("FOUND");
        }
    }

    void Update()
    {

        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        if ((sceneName == "race") && (!hasLoaded))
        {
            gameObject.transform.position = new Vector3(5, 1, 55);
            gameObject.transform.rotation = new Quaternion(0,0,0,0);
            hasLoaded = true;
            Debug.Log("RACE");
        }
    }
}
