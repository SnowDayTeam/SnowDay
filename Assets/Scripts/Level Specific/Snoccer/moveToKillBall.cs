using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToKillBall : MonoBehaviour {

	public bool dead;
	public GameObject target;
    
	void Update () {
		if (dead == true){
			GetComponent<MeshRenderer>().enabled = false;
			target = GameObject.Find("Ball Remover");
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 25 * Time.deltaTime);
		} 	
	}
}
