using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.CharacterController;
using SnowDay.Diego.GameMode;


public class CTFGameManager : ModeManager
{
    [HideInInspector]
    public float RedTeamScore;
    [HideInInspector]
    public float BlueTeamScore;
    
 
    [System.Serializable]
    public class team : BaseTeam
    {
        GoalZone zone;
        //public List<PlayerController> players;
        // public SpawnLocation[] spawnLocations;
        // public int score;
    }
    public team[] Teams;
    public override BaseTeam[] getTeam()
    {
        return Teams;
    }
    //for flag spawning
    public int MaxFlags=2;
    public int currentFlags=1;

    // Use this for initialization
    public override void Start()
    {       
        base.Start();
        for (int i = 0; i < Teams.Length; i++)
        {
            for (int j = 0; j < Teams[i].players.Count; j++)
            {
                Teams[i].players[j].GetComponentInChildren<FlagController>().team = i;
            }
        }
    }
	

    public void CheckWinner()
    {       
        if (RedTeamScore > BlueTeamScore)
        {
            Debug.Log("red wins");   
        }
        else
        {
            Debug.Log("blue wins"); 
        }       
    }
}
