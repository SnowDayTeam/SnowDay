using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.CharacterController;
using SnowDay.Diego.GameMode;
public class ShovelWarManager : ModeManager
{
   
    ShovelWarGAMEHUD GAMEHUD;
    [System.Serializable]
    public class team : BaseTeam
    {
        public SnowPlane snowPlane;
      
    }
    public team[] Teams;

    // Use this for initialization
    void Start ()
    {
        AllPlayers = GameModeController.GetInstance().GetActivePlayers();
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
                Teams[i].players[j].GetComponentInChildren<SnowTackScript>().mySnowPlane = Teams[i].snowPlane;
            }
        }

      
        //2 foot transforms 


    }

}
