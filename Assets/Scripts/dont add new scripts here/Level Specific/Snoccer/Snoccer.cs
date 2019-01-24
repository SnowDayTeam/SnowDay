using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snoccer : MonoBehaviour
{
  
	public int goalsToWin = 3;

//	public bool teamOneWins;
	//public bool teamTwoWins;

    SoccerManager manager;


    // Use this for initialization
    void Start()
    {
        manager = FindObjectOfType<SoccerManager>();
    }

    // Update is called once per frame
    void Update () {
	
	}

   
}
