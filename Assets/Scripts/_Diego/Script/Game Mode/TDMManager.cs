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
        public SnowDayCamera cam;
        int[] teamScore;
        public Transform ScriptsPrefab;
        public int GetTeamScore(int teamID)
        {
            return teamScore[teamID];
        }

        /// <summary>
        /// Increase teams score based on the given ID and points amount
        /// </summary>
        /// <param name="score">Score to increase current team score by</param>
        /// <param name="teamID">ID of the team to add the score to.</param>
        public void IncreaseTeamScore(int score, int teamID)
        {
            teamScore[teamID] += score;
        }

        private void Start()
        {
            List<PlayerController> players  = GameModeController.GetInstance().GetActivePlayers();
            cam.SetTargetPlayers(players);
            cam.Initialize();

            for(int i = 0; i < players.Count; i++)
            {
                Debug.Log(players[i].gameObject.name);
                Instantiate(ScriptsPrefab, players[i].gameObject.transform.GetChild(0).GetChild(2),false);
            }
        }

        private void Update()
        {
            //Debug.Log("TDM Manager - Team Scores: " + teamScore[0] + " , "+ teamScore[1]);
        }
    }
    
}
