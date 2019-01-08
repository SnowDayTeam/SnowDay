using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.CharacterController;
using SnowDay.Diego.GameMode;
public class ShovelWarManager : MonoBehaviour
{
    List<PlayerController> AllPlayers;
    public Transform ScriptsPrefab;
    ShovelWarGAMEHUD GAMEHUD;
    [System.Serializable]
    public struct team
    {
        public TeamManager snowPlane;
        public List<PlayerController> players;
        public int score;
    }
    public team[] Teams;

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
            AllPlayers[i].GetComponentInChildren<SnowTackScript>().myTeam = Teams[0].snowPlane;


        }
        for (int i = midPt; i < numplayers; i++)
        {
            Debug.Log("team 2");
            Teams[1].players.Add(AllPlayers[i]);
            AllPlayers[i].GetComponentInChildren<SnowTackScript>().myTeam = Teams[1].snowPlane;
        }
        // Teams[0].playersAlive = Teams[0].players.Count;
        //  Teams[1].playersAlive = Teams[1].players.Count;


    }
    // Use this for initialization
    void Start ()
    {
        AllPlayers = GameModeController.GetInstance().GetActivePlayers();
        for (int i = 0; i < AllPlayers.Count; i++)
        {
            Debug.Log(AllPlayers[i].gameObject.name);
            Instantiate(ScriptsPrefab, AllPlayers[i].gameObject.transform.GetChild(0).GetChild(2), false);
        }
        teamSplit();
        //link snowteam manager
        //2 foot transforms 


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
