using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detention_JailedPlayersCounter : MonoBehaviour {

    [SerializeField] int jailedPlayers;
    [Range(1,4)]
    [SerializeField] int numKids;
    private Detention_TimerScript detTimer;

    // Use this for initialization
    void Start () {
        jailedPlayers = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (jailedPlayers >= numKids) {
            Debug.Log("Teachers Win");
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            jailedPlayers++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            jailedPlayers--;
        }
    }
}
