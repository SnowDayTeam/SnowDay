using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTFUI : MonoBehaviour {
    //Object References
    [SerializeField]
    CTFGameManager GM;
    [SerializeField]
    Text TimeTXT;
    [SerializeField]
    Text RedScoreTXT;
    [SerializeField]
    Text BlueScoreTXT;
    //win screen
    [SerializeField]
    GameObject winscreen;
    [SerializeField]
    Text WinTXT;
    [SerializeField]
    Color Red;
    [SerializeField]
    Color Blue;

    //Time
    int MatchLength=120;
    int TimeRemaining;
    float startTime;
    bool Winnerdecided = false;






    // Use this for initialization
    void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if(MatchLength- (Time.time - startTime) > 0)
        {
            TimeTXT.text = (Mathf.RoundToInt( MatchLength - (Time.time- startTime))).ToString();
            BlueScoreTXT.text = "B " + GM.BlueTeamScore;
            RedScoreTXT.text = GM.RedTeamScore + " R";

        }

        else if(Winnerdecided==false)
        {
            CheckWinner();
        }
         
   
		
	}

    void CheckWinner()
    {
        Winnerdecided = true;
        if (GM.RedTeamScore > GM.BlueTeamScore)
        {
            winscreen.SetActive(true);
            WinTXT.text = "RED WINS!";
            WinTXT.color = Red;

        }

        else
        {
            winscreen.SetActive(true);
            WinTXT.text = "BLUE WINS!";
            WinTXT.color = Blue;
            


        }
    }
}
