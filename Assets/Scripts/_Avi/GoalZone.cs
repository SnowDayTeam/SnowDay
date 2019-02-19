using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    public int Team;
    // public Color color;

    // Use this for initialization called from CaptureTheFlagGamemodeManager
    public void Setup ()
    {

        GetComponent<MeshRenderer>().material.color = FindObjectOfType<CaptureTheFlagGamemodeManager>().GetTeams()[Team].TeamColor;

        //   GetComponent<MeshRenderer>().material.color = color;
    }
  
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
