using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    public bool BlueWins;
    public bool YellowWins;
    public bool PurpleWins;
    public bool RedWins;
    private string color;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0;
            color = other.GetComponent<PlayerData>().PColor;
            switch (color)
            {
                case "red":
                    RedWins = true;
                    FindObjectOfType<KingOfTheHillUI>().SetWinText("Red Wins");

                    break;

                case "blue":
                    BlueWins = true;
                    FindObjectOfType<KingOfTheHillUI>().SetWinText("Blue Wins");
                    break;

                case "yellow":
                    YellowWins = true;
                    FindObjectOfType<KingOfTheHillUI>().SetWinText("Yellow Wins");
                    break;

                case "purple":
                    PurpleWins = true;
                    FindObjectOfType<KingOfTheHillUI>().SetWinText("Purple Wins");
                    break;
            }
        }
    }
}
