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
    public class team : BaseTeam
    {
        public int playersAlive;
        // public List<PlayerController> players;
        // public int score;
    }
    public team[] Teams;
      
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
       
    private void Start()
    {
        AllPlayers = GameModeController.GetInstance().GetActivePlayers();
        cam.SetTargetPlayers(AllPlayers);
        cam.Initialize();
        for (int i = 0; i < AllPlayers.Count; i++)
        {
            Debug.Log(AllPlayers[i].gameObject.name);
            Instantiate(ScriptsPrefab, AllPlayers[i].gameObject.transform.GetChild(0).GetChild(2),false);
        }
        teamSplit(Teams);

        for (int i = 0; i < Teams.Length; i++)
        {
            for (int j = 0; j < Teams[i].players.Count; j++)
            {
                Teams[i].players[j].GetComponentInChildren<PlayerActor>().TeamID = i;
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
    

