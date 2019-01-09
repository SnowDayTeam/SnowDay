using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelWarGAMEHUD : MonoBehaviour
{
    [SerializeField]
    Text Team1;
    [SerializeField]
    Text Team2;
    [SerializeField]
    TeamManager Team1DriveWay;
    [SerializeField]
    TeamManager Team2DriveWay;
    [SerializeField]
    Text Timer;
    [SerializeField]
    bool SnowClearCondition = false;
    [SerializeField]
    GameObject WinScreen;
    [SerializeField]
    int MatchLength = 5;
    int TimeRemaining;
    float LastSecond=0;






    // Use this for initialization
    void Start () {
        TimeRemaining = MatchLength;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (TimeRemaining > 0)
        {
            if (Time.time - LastSecond > 1)
            {
                MatchTimer();
                LastSecond = Time.time;

            }

        
        }
        else
        {
            CheckForWinner();
        }

     

        SetTeamScores();
        
	}

    void SetTeamScores()
    {
        Team1.text = "Red :" + Mathf.RoundToInt( Team1DriveWay.RedPixelCounter / 65536 * 100)+ "%";
        //Debug.Log(Team1DriveWay.GetComponent<TeamManager>().RedPixelCounter / 65536 * 100);
        Team2.text = "Blue :" + Mathf.RoundToInt (Team2DriveWay.RedPixelCounter / 65536 * 100) + "%";

        //Note sure if i want this to be a win condition
        if (SnowClearCondition)
        {
            
            if (Mathf.RoundToInt(Team1DriveWay.RedPixelCounter / 65536 * 100) > 99)
            {
                //redWins
                Winner(true);
            }

            if (Mathf.RoundToInt(Team2DriveWay.RedPixelCounter / 65536 * 100) > 99)
            {
                //BlueWins
                Winner(false);
            }
        }
 

    }
    //move to manager 
    void MatchTimer()
    {
        TimeRemaining--;
        float minutes = Mathf.Floor(TimeRemaining / 60);
        float seconds = TimeRemaining % 60;

        Timer.text = minutes + ":" + seconds;

        


    }

    //move to manager 
    void CheckForWinner()
    {
        if (Team1DriveWay.RedPixelCounter> Team2DriveWay.RedPixelCounter)
        {
            //Red Team Wins
            Winner(true);
        }

        else if(Team2DriveWay.RedPixelCounter> Team1DriveWay.RedPixelCounter)
        {
            //blue team wins
            Winner(false);
        }

        else if(Team2DriveWay.RedPixelCounter == Team1DriveWay.RedPixelCounter)
        {
            TimeRemaining = 30;
        }
    }

    void Winner(bool RedWins)
    {
        if (RedWins)
        {
            WinScreen.SetActive(true);
            WinScreen.GetComponentInChildren<Text>().text = "RED WINS!";
        }
        else
        {
            WinScreen.SetActive(true);
            WinScreen.GetComponentInChildren<Text>().text = "BLUE WINS!";
        }
    }
}
