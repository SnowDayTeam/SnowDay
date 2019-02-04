using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.GameMode;

public class SoccerManager : ModeManager
{
    [System.Serializable]
    public class team : BaseTeam
    {
        [Tooltip("garage to score into")]
        /// <summary>
        /// garage to score into
        /// </summary>
        public Garage garage;
        //public int playersAlive;
        // public List<PlayerController> players;
        // public int score;
    }
    public team[] Teams;
    public override BaseTeam[] getTeam()
    {
        return Teams;
    }
    public int  goalsToWin = 3;
    public void increaseScore(int teamID)
    {
        Teams[teamID].score++;
    }
    // Use this for initialization
    public override void Start()
    {
        base.Start();

        for (int i = 0; i < Teams.Length; i++)
        {
            Teams[i].garage.TeamID = i;

            for (int j = 0; j < Teams[i].players.Count; j++)
            {
                //Teams[i].players[j].GetComponentInChildren<SnowTackScript>().mySnowPlane = Teams[i].snowPlane;
            }
        }

    }

    // Update is called once per frame
    void Update ()
    {

        for (int i = 0; i < Teams.Length; i++)
        {
            if(Teams[i].score >= goalsToWin)
            {
                Debug.Log("team " + i + " won ");
            }

        }

     
    }
}
