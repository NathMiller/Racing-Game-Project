using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject Panel;
    public TMPro.TMP_Text buttonLabel;
    public GameObject massButton;
    public TMPro.TMP_Text massLabel;
    public TMPro.TMP_Text massUnaffordLabel;
    public GameObject speedButton;
    public TMPro.TMP_Text speedLabel;
    public TMPro.TMP_Text speedUnaffordLabel;
    public GameObject DMDSButton;
    public TMPro.TMP_Text DMDSLabel;
    public TMPro.TMP_Text DMDSUnaffordLabel;
    public GameObject SUMUButton;
    public TMPro.TMP_Text SUMULabel;
    public TMPro.TMP_Text SUMUUnaffordLabel;
    private GameplayManager gameplayManager;

    void Awake()
    {
        gameplayManager = GameObject.FindObjectOfType<GameplayManager>();
    }

    public void shopOpen()
    {


        if (Panel != null)
        {

            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
            if (isActive)
            {
                Time.timeScale = 1;
                buttonLabel.text = "Open shop";
                buttonLabel.alignment = TMPro.TextAlignmentOptions.Center;

            } else
            {
                Time.timeScale = 0;
                buttonLabel.text = "Close shop";
                buttonLabel.alignment = TMPro.TextAlignmentOptions.Center;
            }
        }
        
    }

    public void buyMass()
    {
        int score = gameplayManager.newScore;

        if (score >= 300)
        {
            bool upgradeMass = true;
            score = score - 300;
            gameplayManager.UpdateScore(score);
            gameplayManager.UpdateMass(upgradeMass);
            massLabel.text = "";
            massUnaffordLabel.text = "";
            massButton.SetActive(false);
            
        } else
        {
            massUnaffordLabel.text = "Can't afford!";
       
        }

    }

    public void buySpeed()
    {
        int score = gameplayManager.newScore;
        if (score >= 300)
        {
            bool upgradeSpeed = true;
            score = score - 300;
            gameplayManager.UpdateScore(score);
            gameplayManager.UpdateSpeed(upgradeSpeed);
            speedLabel.text = "";
            speedUnaffordLabel.text = "";
            speedButton.SetActive(false);

        }
        else
        {
            speedUnaffordLabel.text = "Can't afford!";

        }
    }

    public void buyMassDownSpeedDown()
    {
        int score = gameplayManager.newScore;
        if (score >= 500)
        {
            bool upgradeMassDownSpeedDown = true;
            score = score - 500;
            gameplayManager.UpdateScore(score);
            gameplayManager.UpdateMassDownSpeedDown(upgradeMassDownSpeedDown);
            DMDSLabel.text = "";
            DMDSUnaffordLabel.text = "";
            DMDSButton.SetActive(false);
            Debug.Log("has been purchased");

        }
        else
        {
            DMDSUnaffordLabel.text = "Can't afford!";

        }
    }

    public void buySpeedUpMassUp()
    {
        int score = gameplayManager.newScore;
        if (score >= 500)
        {
            bool upgradeSpeedUpMassUp = true;
            score = score - 500;
            gameplayManager.UpdateScore(score);
            gameplayManager.UpdateSpeedUpMassUp(upgradeSpeedUpMassUp);
            SUMULabel.text = "";
            SUMUUnaffordLabel.text = "";
            SUMUButton.SetActive(false);
            Debug.Log("has been purchased");

        }
        else
        {
            SUMUUnaffordLabel.text = "Can't afford!";

        }
    }
}
