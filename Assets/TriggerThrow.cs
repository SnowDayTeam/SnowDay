using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;   

public class TriggerThrow : MonoBehaviour {

    Animator anim;
    FullBodyBipedIK IK;
    public float reachTime;
    public float downTime;
    bool pickingUp;
    bool canSetHandTargets;
    Transform pickUpPoint;
    float weight;
    float delayTime = 0;
    bool reachBack;

    void Start () {
        anim = GetComponent<Animator>();
        IK = GetComponent<FullBodyBipedIK>();
        pickUpPoint  = transform.Find("Pickup Point");
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("ThrowHigh");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            pickingUp = true;

        }

        if(pickingUp == true)
        {
            PickUp();
        }
    }

    void ThrowSnow()
    {
        print("Threw Snow");
    }

    void PickUp()
    {
            IK.solver.leftHandEffector.target = pickUpPoint;
            IK.solver.rightHandEffector.target = pickUpPoint;
        IK.solver.leftHandEffector.positionWeight = weight;
        IK.solver.rightHandEffector.positionWeight = weight;
        //IK.solver.leftShoulderEffector.positionWeight = weight / 10;
        //IK.solver.rightShoulderEffector.positionWeight = weight / 10;


        if (weight < 1 && reachBack == false)
        {
            weight += Time.deltaTime * reachTime;
            print("picking up");
        }

        if (weight >= 1 && reachBack == false)
        {
            print("Waiting");

            delayTime += Time.deltaTime;
            if(delayTime >= downTime)
            {
                reachBack = true;
            }

        }

        if (reachBack == true)
        {
            print("Reaching Back");

            weight -= Time.deltaTime * reachTime;
            if(weight <= 0)
            {
                delayTime = 0;
                weight = 0;
                IK.solver.leftHandEffector.target = null;
                IK.solver.rightHandEffector.target = null;
                reachBack = false;
                pickingUp = false;
                print("Done");
            }
        }
    }
}
