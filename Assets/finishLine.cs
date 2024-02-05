using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLine : MonoBehaviour
{
    public bool finished = false;
    public bool won;
    public TMPro.TMP_Text status;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER WON");
            finished = true;
            won = true;
            EndGame(finished, won);
            
        } else
        {
            finished = true;
            won = false;
            EndGame(finished, won);
        }
    }

    void EndGame(bool finished, bool won)
    {
        Time.timeScale = 0;

        if (won)
        {
            status.text = "You won!";
            status.alignment = TMPro.TextAlignmentOptions.Center;
        }
        else if (!won)
        {
            status.text = "You lost!";
            status.alignment = TMPro.TextAlignmentOptions.Center;
        }

    }
}
