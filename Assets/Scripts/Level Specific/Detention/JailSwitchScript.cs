using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailSwitchScript : MonoBehaviour {

    private bool isGateOpen;
    private Vector3 gatePos;
    public GameObject gate;
  
	// Use this for initialization
	void Start () {
        isGateOpen = false;
        gatePos = gate.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (isGateOpen)
        {
           // Debug.Log("Gates OPEN");
            if (gate.transform.position.y < 5.0f)
            {
                gatePos.y += 2.0f * Time.deltaTime;
                gate.transform.position = gatePos;
            }
        }

        if (!isGateOpen)
        {
           // Debug.Log("Gates CLOSED");
            if (gate.transform.position.y > 0.73f)
            {
                gatePos.y -= 2.0f * Time.deltaTime;
                gate.transform.position = gatePos;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isGateOpen = true;
        }

        if (other.gameObject.tag == "Snow")
        {
            isGateOpen = false;
        }
    }
}
