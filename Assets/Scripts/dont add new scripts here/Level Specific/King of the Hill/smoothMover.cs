using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothMover : MonoBehaviour {


	public Transform newPos;
	public float transitionSpeed = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, newPos.position, Time.deltaTime * transitionSpeed);
	}
}
