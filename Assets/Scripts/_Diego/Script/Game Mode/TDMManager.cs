using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.Singleton;
using SnowDay.Diego.CharacterController;

namespace SnowDay.Diego.GameMode
{
    /// <summary>
    /// Keeps track of what team has the most points
    /// </summary>
    /// <remarks>
    /// <para>Currently Only Handles 2 Teams</para>
    /// </remarks>
    public class TDMManager : MonoBehaviour
    {
       // public int numplayers = 4;
        
        List<PlayerController> AllPlayers;
        public struct team
        {
            public int playersAlive;
            public List<PlayerController> players;
            public int score;
        }
        public team[] Teams;
        public SnowDayCamera cam;
        //int[] teamScore;
        public Transform ScriptsPrefab;
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

        }
        [ContextMenu("split")]
        private void teamSplit(int numTeam = 2)
        {
            Teams = new team[numTeam];
            if (numTeam == 2)
            {
                int numplayers = AllPlayers.Count;
                int midPt = numplayers / numTeam;
                Teams[0].players = new List<PlayerController>();
                Teams[1].players = new List<PlayerController>();
                for (int i = 0; i < midPt; i++)
                {
                    Debug.Log("team 1");
                    Teams[0].players.Add(AllPlayers[i]);
                    AllPlayers[i].GetComponentInChildren<PlayerActor>().TeamID = 0;
                }
                for (int i = midPt; i < numplayers; i++)
                {
                    Debug.Log("team 2");
                    Teams[1].players.Add(AllPlayers[i]);
                    AllPlayers[i].GetComponentInChildren<PlayerActor>().TeamID = 1;
                }
                Teams[0].playersAlive = Teams[0].players.Count;
                Teams[1].playersAlive = Teams[1].players.Count;
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
            teamSplit();

        }

        private void Update()
        {
            //Debug.Log("TDM Manager - Team Scores: " + teamScore[0] + " , "+ teamScore[1]);
        }
    }
    
}
