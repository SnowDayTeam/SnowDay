using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killBall : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "SnowBall"){
			Destroy (other);			
		}
	}
}
