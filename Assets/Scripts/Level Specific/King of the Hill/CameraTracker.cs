using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour {

	public Transform player;
	// Update is called once per frame
	void Update () {
		transform.position = player.position;
	}
}
