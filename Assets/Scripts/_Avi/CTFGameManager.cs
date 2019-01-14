using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.CharacterController;
using SnowDay.Diego.GameMode;


public class CTFGameManager : MonoBehaviour {
    [HideInInspector]
    public float RedTeamScore;
    [HideInInspector]
    public float BlueTeamScore;
    public Transform ScriptsPrefab;
    public SnowDayCamera cam;
    List<PlayerController> AllPlayers;
    [System.Serializable]
    public struct team
    {
     
        public List<PlayerController> players;
        public SpawnLocation[] spawnLocations;
        public int score;

    }
    public team[] Teams;
    //for flag spawning
    public int MaxFlags=2;
    public int currentFlags=1;
    




    // Use this for initialization
    void Start () {
        AllPlayers = GameModeController.GetInstance().GetActivePlayers();
        cam.SetTargetPlayers(AllPlayers);
        cam.Initialize();

        for (int i = 0; i < AllPlayers.Count; i++)
        {
            Debug.Log(AllPlayers[i].gameObject.name);
            Instantiate(ScriptsPrefab, AllPlayers[i].gameObject.transform.GetChild(0).GetChild(2), false);
        }
        teamSplit();
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    private void teamSplit()
    {
        int numTeam = 2;
        int numplayers = AllPlayers.Count;
        int midPt = numplayers / numTeam;
        Teams[0].players = new List<PlayerController>();
        Teams[1].players = new List<PlayerController>();
        for (int i = 0; i < midPt; i++)
        {
            Debug.Log("team 1");
            Teams[0].players.Add(AllPlayers[i]);
            AllPlayers[i].GetComponentInChildren<FlagController>().team = 1;
            //AllPlayers[i].GetComponentInChildren<SnowTackScript>().myTeam = Teams[0].snowPlane;
            AllPlayers[i].MoveCharacter(Teams[0].spawnLocations[0].transform.position);
           

        }
        for (int i = midPt; i < numplayers; i++)
        {
            Debug.Log("team 2");
            Teams[1].players.Add(AllPlayers[i]);
            AllPlayers[i].GetComponentInChildren<FlagController>().team = 2;
            //AllPlayers[i].GetComponentInChildren<SnowTackScript>().myTeam = Teams[1].snowPlane;
            AllPlayers[i].MoveCharacter(Teams[1].spawnLocations[0].transform.position);

        }
        // Teams[0].playersAlive = Teams[0].players.Count;
        //  Teams[1].playersAlive = Teams[1].players.Count;

        
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
