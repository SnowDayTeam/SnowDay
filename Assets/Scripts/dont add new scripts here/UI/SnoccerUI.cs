using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnoccerUI : MonoBehaviour {
    public GameObject GM;
    public GameObject WinUI;
    public Text Team1TXT;
    public Text Team2TXT;
    public Text Win;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // gets the score of the game
        GetScore();
        //if a team has won it will bring up the win screen and pause the game
        if (GM.GetComponent<Snoccer>().teamOneWins == true)
        {
            WinUI.SetActive(true);
            Win.text = "Blue Team Wins!";
            Time.timeScale = 0;
        }

        else if (GM.GetComponent<Snoccer>().teamTwoWins == true)
        {
            WinUI.SetActive(true);
            Win.text = "Red Team Wins!";
            Time.timeScale = 0;
        }
	}
    //gets team 1 and 2 score
   private void GetScore()
    {
		Team1TXT.text = ("Red Team: " + GM.GetComponent<Snoccer>().Team1_Points);
		Team2TXT.text = ("Blue Team: " + GM.GetComponent<Snoccer>().Team2_Points);
    }
    //reloads current scene
    public void Restart()
    {
        SceneManager.LoadScene(1);
		Time.timeScale = 0;
    }
    //go's back to main menu
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
