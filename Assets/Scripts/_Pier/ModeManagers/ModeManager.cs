using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.CharacterController;
using SnowDay.Diego.GameMode;
public class ModeManager : MonoBehaviour
{
    protected List<PlayerController> AllPlayers;
    public Transform ScriptsPrefab;
    public SnowDayCamera cam;

    [System.Serializable]
    public class BaseTeam
    {
        public List<PlayerController> players;
        public SpawnLocation[] spawnLocations;
        public int score;
        public Color teamColor;
    }

    public virtual BaseTeam[] getTeam()
    {
        return null;
    }

    public virtual void Start()
    {
        AllPlayers = GameModeController.GetInstance().GetActivePlayers();
        if(cam!= null)
        {
            cam.SetTargetPlayers(AllPlayers);
            cam.Initialize();
        }

        for (int i = 0; i < AllPlayers.Count; i++)
        {
            Debug.Log(AllPlayers[i].gameObject.name);
            Instantiate(ScriptsPrefab, AllPlayers[i].gameObject.transform.GetChild(0).GetChild(2), false);
        }
        teamSplit(getTeam());
    }
   // public BaseTeam[] Teams;
    protected void teamSplit(BaseTeam[] Teams)
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
            //   AllPlayers[i].GetComponentInChildren<SnowTackScript>().mySnowPlane = Teams[0].snowPlane;
            AllPlayers[i].MoveCharacter(Teams[0].spawnLocations[i].transform.position);

        }
        for (int i = midPt; i < numplayers; i++)
        {
            Debug.Log("team 2");
            Teams[1].players.Add(AllPlayers[i]);
            //    AllPlayers[i].GetComponentInChildren<SnowTackScript>().mySnowPlane = Teams[1].snowPlane;
            AllPlayers[i].MoveCharacter(Teams[1].spawnLocations[i - midPt].transform.position);

        }


    }
}
