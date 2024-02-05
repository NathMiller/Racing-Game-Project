using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Rigidbody Car;
    public TMPro.TMP_Text scoreLabel;
    

    void Update()
    {
        float score = 0;
        scoreLabel.text = "" + (int)score;
        scoreLabel.alignment = TMPro.TextAlignmentOptions.Center;
    }
}
