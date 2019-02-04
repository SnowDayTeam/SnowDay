using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCamFollow : MonoBehaviour {

    public Transform sled;
    public float x;
    public float y;
    public float z;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(sled.transform.position.x + x, sled.transform.position.y + y, sled.transform.position.z + z);
        transform.LookAt(sled);
	}
}
