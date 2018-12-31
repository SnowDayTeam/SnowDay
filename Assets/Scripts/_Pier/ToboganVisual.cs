using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToboganVisual : MonoBehaviour
{
    public float offset;
    public Rigidbody TargetRigidbody;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(TargetRigidbody.position.x, TargetRigidbody.position.y + offset, TargetRigidbody.position.z);
        transform.rotation = Quaternion.LookRotation(TargetRigidbody.velocity.normalized, Vector3.up);
	}
}
