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
    public override BaseTeam[] getTeam()
    {
        return Teams;
    }
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        for (int i = 0; i < Teams.Length; i++)
        {
            for (int j = 0; j < Teams[i].players.Count; j++)
            {
                //  Teams[i].players[j].GetComponentInChildren<SnowTackScript>().myTeam = Teams[0].snowPlane;
                Teams[i].players[j].GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Teams[i].teamColor;
                Teams[i].players[j].GetComponentInChildren<SnowTackScript>().mySnowPlane = Teams[i].snowPlane;
               // Debug.Log("set SnowTak");
            }
        }
        //2 foot transforms 
    }

}
