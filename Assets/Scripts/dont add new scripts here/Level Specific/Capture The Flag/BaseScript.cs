using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BaseScript : MonoBehaviour {

    private int redTeamPoints = 0;
    private int blueTeamPoints = 0;

    public Text redTeamPointsText;
    public Text blueTeamPointsText;
    public GameObject redTeamWinScreen;
    public GameObject blueTeamWinScreen;

    void OnTriggerEnter(Collider other)
    {

        if (gameObject.tag == "RedTeamBase")
        {
            if (other.tag == "RedFlag")
            {
                redTeamPoints += 1;
                other.tag = "Untagged";
                //other.GetComponent<FlagPickup>().isAtBase = true;
                redTeamPointsText.text = redTeamPoints.ToString();
                other.transform.parent = gameObject.transform;
            }
         }

        if (gameObject.tag == "BlueTeamBase")
        {
            if (other.tag == "BlueFlag")
            {
                blueTeamPoints += 1;
                other.tag = "Untagged";
                //other.GetComponent<FlagPickup>().isAtBase = true;
                blueTeamPointsText.text = blueTeamPoints.ToString();           
                other.transform.parent = gameObject.transform;

            }
        }
    }

    void Update()
    {
        if(redTeamPoints == 3)
        {
            redTeamWinScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (blueTeamPoints == 3)
        {
            blueTeamWinScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
        
    }
}
