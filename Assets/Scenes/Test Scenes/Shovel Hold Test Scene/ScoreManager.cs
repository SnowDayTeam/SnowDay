using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text Team1Score;
    public Text Team2Score;

    public float scoreIncreaseAmount = 0.5f;
    public float team1Score = 0.0f;
    public float team2Score = 0.0f;

    public void ScoreUpdate (int TeamNumber) {
        if (TeamNumber == 1)
        {
            team1Score += scoreIncreaseAmount;
            Team1Score.text = "Red Team Score: " + team1Score.ToString();
        }

        if (TeamNumber == 2)
        {
            team2Score += scoreIncreaseAmount;
            Team2Score.text = "Blue Team Score: " + team2Score.ToString();

        }
    }




}
