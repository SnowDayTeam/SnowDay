using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorRotation : MonoBehaviour {

    Vector3 camPos;
    float thisHeight;
	// Use this for initialization
	void Start () {
        camPos = Camera.main.transform.position;
        camPos.y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(camPos);
    }
}
//var targetObj: Transform;
//   ...
//var point: Vector3 = targetObj.position;
//point.y = 0.0;
//transform.LookAt(point);