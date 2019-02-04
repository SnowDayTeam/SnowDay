using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToboganVisual : MonoBehaviour
{
    public float offset;
    public Rigidbody TargetRigidbody;
    private int normalIndex = 0;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = new Vector3(TargetRigidbody.position.x, TargetRigidbody.position.y + offset, TargetRigidbody.position.z);
        transform.rotation = Quaternion.LookRotation(TargetRigidbody.velocity.normalized, Vector3.up);
	}
    //Vector3 GetUpVector()
    //{
    //    Vector3 average = new Vector3();

    //    RaycastHit rayHit;
    //    Vector3 downwardCastVector = transform.TransformDirection(Vector3.down);
    //    if (Physics.Raycast(transform.position, downwardCastVector, out rayHit, Mathf.Infinity))
    //    {
    //        normalIndex = (normalIndex + 1) % 3;
    //        normal[normalIndex] = rayHit.normal;
    //        average = ((normal[0] + normal[1] + normal[2]) / 3);

    //        if (transform.up.y - rayHit.normal.y < 0.005f) { return; }
    //        transform.rotation = Quaternion.FromToRotation(transform.up, average) * transform.rotation;
    //    }

    //}
}
