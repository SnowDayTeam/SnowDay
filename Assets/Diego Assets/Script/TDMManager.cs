using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMManager : Singleton<TDMManager> {

    int[] teamScore;

    public int GetTeamScore(int teamID)
    {
        return teamScore[teamID];
    }

    public void IncreaseTeamScore(int score , int teamID)
    {
        teamScore[teamID] += score;
    }

    private void Start()
    {
        teamScore = new int[2];
    }

    private void Update()
    {
        Debug.Log("TDM Manager - Team Scores: " + teamScore[0] + " , "+ teamScore[1]);
    }
}
