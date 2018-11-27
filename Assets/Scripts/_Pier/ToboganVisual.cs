using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToboganVisual : MonoBehaviour
{
    public Rigidbody TargetRigidbody;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = TargetRigidbody.position;
        transform.rotation = Quaternion.LookRotation(TargetRigidbody.velocity.normalized, Vector3.up);
	}
}
