using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.Singleton;

/// <summary>
/// Keeps track of what team has the most points
/// </summary>
/// <remarks>
/// <para>Currently Only Handles 2 Teams</para>
/// </remarks>
public class TDMManager : Singleton<TDMManager> {

    int[] teamScore;

    public int GetTeamScore(int teamID)
    {
        return teamScore[teamID];
    }

    /// <summary>
    /// Increase teams score based on the given ID and points amount
    /// </summary>
    /// <param name="score">Score to increase current team score by</param>
    /// <param name="teamID">ID of the team to add the score to.</param>
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
