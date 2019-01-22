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
        Color teamColor;
        GoalZone zone;
        //public List<PlayerController> players;
        // public SpawnLocation[] spawnLocations;
        // public int score;

    }
    public team[] Teams;
    //for flag spawning
    public int MaxFlags=2;
    public int currentFlags=1;
    
    // Use this for initialization
    void Start ()
    {
        AllPlayers = GameModeController.GetInstance().GetActivePlayers();
        cam.SetTargetPlayers(AllPlayers);
        cam.Initialize();

        for (int i = 0; i < AllPlayers.Count; i++)
        {
            Debug.Log(AllPlayers[i].gameObject.name);
            Instantiate(ScriptsPrefab, AllPlayers[i].gameObject.transform.GetChild(0).GetChild(2), false);
        }
        teamSplit(Teams);

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
