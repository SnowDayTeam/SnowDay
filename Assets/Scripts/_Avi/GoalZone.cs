using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//name should be refactored to CaptureTheFlagGoal
public class GoalZone : MonoBehaviour
{
    /// <summary>
    /// The team that ownins this goal zone, setup via ctfgame mode manager
    /// </summary>
    [HideInInspector] public CaptureTheFlagGamemodeManager.CaptureTheFlagTeam OwningTeam = null;

    void Start() 
    {
        foreach(Renderer renderer in this.GetComponentsInChildren<Renderer>()) 
        {
            renderer.material.color = this.OwningTeam.TeamColor;
        }
    }

    /// <summary>
    /// Add 1 point to the owning teams' score
    /// </summary>
    public void Score() 
    {
        this.OwningTeam.Score ++;
        GetComponent<AudioSource>().Play();
    }

    //public int Team;
    //public Color color;

    //[HideInInspector] CaptureTheFlagGamemodeManager.TeamBase

    // Use this for initialization called from CaptureTheFlagGamemodeManager
    //public void Setup ()
    //{

    //    GetComponent<MeshRenderer>().material.color = FindObjectOfType<CaptureTheFlagGamemodeManager>().GetTeams()[Team].TeamColor;

    //    //GetComponent<MeshRenderer>().material.color = color;
    //}
  
    // Update is called once per frame
    //void Update () {
        
    //    if (Team == 1)
    //    {
    //        GetComponent<MeshRenderer>().material.color = Color.red;
    //    }
    //    else
    //    {
    //        GetComponent<MeshRenderer>().material.color = Color.blue;
    //    }
    //}
}
