using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectOut : MonoBehaviour {

    public float BackAmount;
    public bool AwayFromCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        if (AwayFromCamera == false)
        {
            collision.gameObject.transform.position -= new Vector3(0, 0, BackAmount);
           
        }

        else
        {
            collision.gameObject.transform.position -= new Vector3(0, 0, -BackAmount);

        }

    }


}
