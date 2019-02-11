using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using SnowDay.Diego.CharacterController;
using SnowDay.Diego.GameMode;


/// <summary>
/// Keeps track of what team has the most points
/// </summary>
/// <remarks>
/// <para>Currently Only Handles 2 Teams</para>
/// </remarks>
public class TDMManager : ModeManager
{
    private bool startCountDown;
    public float GameOverCountDown = 4;
    [Header("UI Text")]
    public Text CountDownText;
    [System.Serializable]
    public class team : BaseTeam
    {
        public int playersAlive;
        // public List<PlayerController> players;
        // public int score;
    }
    public team[] Teams;
    public override BaseTeam[] getTeam()
    {
        return Teams;
    }
    //int[] teamScore;

    public int GetTeamScore(int teamID)
    {
        return Teams[teamID].score;
    }

    /// <summary>
    /// Increase teams score based on the given ID and points amount
    /// </summary>
    /// <param name="score">Score to increase current team score by</param>
    /// <param name="teamID">ID of the team to add the score to.</param>
    public void IncreaseTeamScore(int score, int teamID)
    {
        Teams[teamID].score += score;
    }

    public void playerDeadReport(int teamID)
    {
        Teams[teamID].playersAlive --;
        if (Teams[teamID].playersAlive <= 0)
        {
            Debug.Log("team # " + teamID + " dead");
            startCountDown = true;
            CountDownText.enabled = true;
        }

    }

    public override void Start()
    {
        base.Start();

        for (int i = 0; i < Teams.Length; i++)
        {
            Teams[i].playersAlive = Teams[i].players.Count;
            for (int j = 0; j < Teams[i].players.Count; j++)
            {
                Teams[i].players[j].GetComponentInChildren<PlayerActor>().TeamID = i;
                Teams[i].players[j].GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Teams[i].teamColor;

            }
        }

    }

    private void Update()
    {
        if (startCountDown)
        {
            GameOverCountDown -= Time.deltaTime;
            CountDownText.text = GameOverCountDown.ToString("0");
            if(GameOverCountDown <= 0)
            {
                SceneManager.LoadScene("LevelSelect");
            }
        }
            //Debug.Log("TDM Manager - Team Scores: " + teamScore[0] + " , "+ teamScore[1]);
    }
}
    

