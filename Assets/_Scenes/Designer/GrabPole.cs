using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using RootMotion.Dynamics;

public class GrabPole : MonoBehaviour {

    public BehaviourPuppet puppet;
    public float grabSpeed = 1.3f;
    public bool nearPole;
    public bool grabPole;
    public bool isHolding;
    FullBodyBipedIK IK;
    public Transform pole;
    Transform grabPointL;
    Transform grabPointR;
    public Transform elbowTargetL;
    public Transform elbowTargetR;
    public bool hasFallen;
    public Transform leftHand;
    Vector3 shovelStartPos;
    Quaternion shovelStartRot;

    float shovelReturnTime = 0.5f;

    Transform offsetter;
    float weight = 0;
    public bool isShovelWar;
    bool gettingUp;


    // Use this for initialization
    void Start () {
        IK = GetComponent<FullBodyBipedIK>();
        puppet = transform.parent.GetComponentInChildren<BehaviourPuppet>();


        if (isShovelWar == true)
        {
           
            shovelStartPos = pole.transform.localPosition;
            shovelStartRot = pole.transform.localRotation;

            elbowTargetL = pole.transform.parent.Find("Elbow Direction L");
            elbowTargetR = pole.transform.parent.Find("Elbow Direction R");
            IK.solver.leftArmChain.bendConstraint.bendGoal = elbowTargetL;
            IK.solver.leftArmChain.bendConstraint.weight = 1;
            IK.solver.rightArmChain.bendConstraint.bendGoal = elbowTargetR;
            IK.solver.rightArmChain.bendConstraint.weight = 1;

            Transform[] children = GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                if (child.name == "BND_L_Wrist_JNT")
                {
                  leftHand = child;
                }
            }
            weight = 1;

        }
    }
	
	// Update is called once per frame
	void Update () {
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



            //have the offseter match player direction
            if (isShovelWar == false)
            {
                offsetter.forward = IK.transform.forward;
            }
            else
            {
                //shovel behaviour for unpinning 
                if (puppet.state == BehaviourPuppet.State.Unpinned)
                {
                    print("fall");

                    if (hasFallen == false)
                    {
                       // pole.transform.parent = leftHand;

                        hasFallen = true;
                        //pole.GetComponent<ShovelLerpController>().enabled = false;
                      //  pole.GetComponent<lerpPosition>().enabled = false;
                      //  pole.GetComponent<lerpRotation>().enabled = false;
                    }
                    pole.position = leftHand.position;
                    weight = 0;
                    if (hasFallen == true)
                    {
                        if (weight > 0)
                        {
                           // weight -= Time.deltaTime * grabSpeed;
                        }
                    }
                }

                if (puppet.state == BehaviourPuppet.State.GetUp)
                {
                    print("getting up");

                    hasFallen = false;
                    //  pole.transform.parent = transform.Find("Shovelling Prefab");
                    pole.transform.localPosition = shovelStartPos;//Vector3.Lerp(pole.transform.localPosition, shovelStartPos, shovelReturnTime);
                  //  pole.transform.localRotation = Quaternion.Lerp(pole.transform.localRotation, shovelStartRot, shovelReturnTime);
                    gettingUp = true;
                    weight = 1;
                }
                //if we've stood up after a fall, reset the shovel position
                if (puppet.state == BehaviourPuppet.State.Puppet)
                {
                   if(gettingUp == true)
                    {
                        pole.transform.localPosition = shovelStartPos;
                        pole.transform.localRotation = shovelStartRot;
                        gettingUp = false;
                    }
                }
            }
           

            if (weight < 1)
            {
            //    weight += Time.deltaTime * grabSpeed;
            }

            

        }
        else
        {
            if (weight > 0)
            {
          //      weight -= Time.deltaTime * grabSpeed;
            }
        }
        IK.solver.leftHandEffector.positionWeight = weight; 
        IK.solver.rightHandEffector.positionWeight = weight;
        IK.solver.leftHandEffector.rotationWeight = weight;
        IK.solver.rightHandEffector.rotationWeight = weight;
    }
}
