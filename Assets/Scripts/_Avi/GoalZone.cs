using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GoalZone : MonoBehaviour {
    public float Team;

 









    // Use this for initialization
    void Start () {

   

    }

    // Update is called once per frame
    void Update () {
        
        if (Team == 1)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }



}
