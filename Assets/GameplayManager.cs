using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text attemptText;
    public int newScore;
    public bool massUpgrade;
    public bool speedUpgrade;
    public bool massDownSpeedDownUpgrade;
    public bool speedUpMassUpUpgrade;


    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + (int)score;
        newScore = score;
        
    }

    public void UpdateAttempts(int attempts)
    {
        attemptText.text = "Attempts:  " + (int)attempts + "/3";
    }

    public void UpdateMass(bool upgradeMass)
    {
        if (upgradeMass)
        {
            massUpgrade = true;
        } else
        {
            massUpgrade = false;
        }
        

    }

    public void UpdateSpeed(bool upgradeSpeed)
    {
        if (upgradeSpeed)
        {
            speedUpgrade = true;
        }
        else
        {
            speedUpgrade = false;
        }
    }

    public void UpdateMassDownSpeedDown(bool upgradeMassDownSpeedDown)
    {
        if (upgradeMassDownSpeedDown)
        {
            massDownSpeedDownUpgrade = true;
        }
        else
        {
            massDownSpeedDownUpgrade = false;
        }
    }

    public void UpdateSpeedUpMassUp(bool upgradeSpeedUpMassUp)
    {
        if (upgradeSpeedUpMassUp)
        {
            speedUpMassUpUpgrade = true;
        }
        else
        {
            speedUpMassUpUpgrade = false;
        }
    }




}
