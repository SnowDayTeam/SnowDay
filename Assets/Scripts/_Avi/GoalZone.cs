using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GoalZone : MonoBehaviour
{
    public float Team;
    public Color color;
 









    // Use this for initialization
    void Start () {


     //   GetComponent<MeshRenderer>().material.color = color;
    }
    public void Setup(Color _color)
    {
        color = _color;
        GetComponent<MeshRenderer>().material.color = color;

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
