using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnoccerUI : MonoBehaviour
{

    public GameObject GM;
    public GameObject WinUI;
    public Text Team1TXT;
    public Text Team2TXT;
    public Text Win;
    SoccerManager manager;// = FindObjectOfType<SoccerManager>();


    // Use this for initialization
    void Start ()
    {
        manager = FindObjectOfType<SoccerManager>();
    }
	
    public void DisplayWinText(int teamID)
    {
        WinUI.SetActive(true);

        if (teamID == 0)
        {
            Win.text = "Blue Team Wins!";
        }
        else
        {
            Win.text = "Red Team Wins!";
        }
        Time.timeScale = 0;

    }
    // Update is called once per frame
    void Update ()
    {
        // gets the score of the game
        GetScore();
       
	}
    //gets team 1 and 2 score
   private void GetScore()
   {
        Team1TXT.text = ("Red Team: " + manager.Teams[0].score);
		Team2TXT.text = ("Blue Team: " + manager.Teams[1].score);
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
