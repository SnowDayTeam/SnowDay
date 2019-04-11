using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterLauncher : MonoBehaviour {
    

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
            other.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(100, 70, 0));
        }
    }
}
