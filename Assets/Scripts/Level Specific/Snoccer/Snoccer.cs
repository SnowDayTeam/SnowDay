using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snoccer : MonoBehaviour {

    

    public int Team1_Points;
    public int Team2_Points;
	public int goalsToWin = 3;

	public bool teamOneWins;
	public bool teamTwoWins;

	// Use this for initialization
	void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		if(Team1_Points >= goalsToWin){
			teamOneWins = true;
		}
        
		if (Team2_Points >= goalsToWin)
        {
            teamTwoWins = true;
        }
	}

   
}
