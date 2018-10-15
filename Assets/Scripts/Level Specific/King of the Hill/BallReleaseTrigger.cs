using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReleaseTrigger : MonoBehaviour {

	public GameObject snowBall;

    public Transform spawner;

   
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Player Container")){
			 Instantiate(snowBall, spawner.position,spawner.rotation);
            
		}
	}
}
