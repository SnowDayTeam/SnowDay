using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hillCameraController : MonoBehaviour {

	public float distanceOffset;   
	public float heightOffset;
	public Transform[] pack;
	Transform leader;

    

	void Start()
	{
		//pack[]
	}
	// Use this for initialization


	// Update is called once per frame
	void Update () {
            leader = pack[0];
		    int i;
            for (i = 1; i < pack.Length; i++)
            {
                if (pack[i].transform.position.z > leader.transform.position.z)
                {
                    leader = pack[i];
                }
            }
		//print(leader.gameObject.name);

		transform.position = new Vector3(transform.position.x, leader.position.y + heightOffset, leader.position.z + distanceOffset);
       
        }
	}

