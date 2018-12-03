using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherGrabStudent : MonoBehaviour {

    //replace this public get method
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //idea for dropping kids off inside of the jail on button press
        if (Input.GetKeyDown("joystick button 1")) {
            if (leftHand.transform.childCount > 0 || rightHand.transform.childCount > 0)
            {
                if (rightHand.transform.childCount == 1)
                {
                    rightHand.transform.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    rightHand.transform.GetChild(0).transform.position *= 1.1f;
                    rightHand.transform.DetachChildren();
                }
                if (leftHand.transform.childCount == 1)
                {
                    leftHand.transform.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    leftHand.transform.DetachChildren();
                   

                }

            }
        }
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (leftHand.transform.childCount > 0 && rightHand.transform.childCount > 0)
                return;
            if (leftHand.transform.childCount == 0)
            {
                other.transform.parent = leftHand.gameObject.transform;
                other.transform.position = leftHand.transform.position;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else if (rightHand.transform.childCount == 0) {
                Debug.Log("grab");
                other.transform.parent = rightHand.gameObject.transform;
                other.transform.position = rightHand.transform.position;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            }
        }

        //if we want to use on trigger or oncollision to release kids in jail from teacher hands
        if (other.gameObject.tag == "Car") {
            if (leftHand.transform.childCount > 0 || rightHand.transform.childCount > 0)
            {
                if (rightHand.transform.childCount == 1)
                {
                    rightHand.transform.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    rightHand.transform.DetachChildren();
                }
                if (leftHand.transform.childCount == 1)
                {
                    leftHand.transform.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    leftHand.transform.DetachChildren();


                }

            }
        }
    }

    //not sure how the jail is to be implemented, if teacher can pass through walls, can use ignore collision or layer collision ignores.
    //can add a bool to students isJailed, if jailed, then enable collision, if !isJailed after switch if activated, ignore collision and free students
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Interactive") {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
        }
    }
}
