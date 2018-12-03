using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public bool didGetFlag = false;

    void OnTriggerEnter(Collider other)
   { 
        if (gameObject.tag == "RedTeam" && other.tag == "RedTeamBase" && didGetFlag)
        {
            didGetFlag = false;
        }

        if (gameObject.tag == "BlueTeam" && other.tag == "BlueTeamBase" && didGetFlag){
            didGetFlag = false;
        }

        if (gameObject.tag == "RedTeam" && other.tag == "BlueTeam" && other.GetComponent<PlayerScript>().didGetFlag && didGetFlag)
        {
            didGetFlag = false;
        }

        if (gameObject.tag == "BlueTeam" && other.tag == "RedTeam" && other.GetComponent<PlayerScript>().didGetFlag  && didGetFlag)
        {
            didGetFlag = false;
        }
    }
}
