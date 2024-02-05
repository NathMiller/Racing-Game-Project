using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarControl : MonoBehaviour
{

    private controller Controller;




    private void Awake()
    {
        Controller = GetComponent<controller>();

    }

    private void Update()
    {
        bool AI = true;
        Controller.AICar(AI);
    }


}
