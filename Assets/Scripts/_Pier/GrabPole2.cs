using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using RootMotion.Dynamics;
using SnowDay.Diego.CharacterController;

public class GrabPole2 : MonoBehaviour {

    public BehaviourPuppet puppet;
    public float grabSpeed = 1.3f;
    public bool nearPole;
    public bool grabPole;

    public bool isHolding;
    FullBodyBipedIK IK;

    public Transform pole;
    Transform grabPointL;
    Transform grabPointR;
 


    Transform offsetter;
    float weight = 0;
   // public bool isShovelWar;
    bool gettingUp;


    // Use this for initialization
    void Start ()
    {
        IK = GetComponentInParent<FullBodyBipedIK>();

        puppet = transform.GetComponentInParent<PlayerController>().transform.GetComponentInChildren<BehaviourPuppet>();


       
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(puppet.state);

        //if(leftHand == null)
        //{
        //    foreach (Transform child in transform)
        //    {
        //        print(child.name);
        //        if (child.name == "BND_L_Wrist_JNT")
        //        {
        //            leftHand = child;
        //        }
        //    }
        //}
        //Lerp the IK Weights when we are within weight and grabbing
        if (nearPole == true && grabPole == true)
        {
            //set the offsetter and grab points
            offsetter = pole.Find("Handle Offsetter");
            grabPointL = offsetter.Find("Grab Point L");
            grabPointR = offsetter.Find("Grab Point R");
            IK.solver.leftHandEffector.target = grabPointL;
            IK.solver.rightHandEffector.target = grabPointR;


            if (puppet.state == BehaviourPuppet.State.Unpinned)
            {
                grabPole = false;
            }
           


            if (weight < 1)
            {
                weight += Time.deltaTime * grabSpeed;
            }

            

        }
        else
        {
            if (weight > 0)
            {
                weight -= Time.deltaTime * grabSpeed;
            }
        }
        IK.solver.leftHandEffector.positionWeight = weight; 
        IK.solver.rightHandEffector.positionWeight = weight;
        IK.solver.leftHandEffector.rotationWeight = weight;
        IK.solver.rightHandEffector.rotationWeight = weight;
    }
}
