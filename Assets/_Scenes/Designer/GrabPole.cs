using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class GrabPole : MonoBehaviour {

    public float grabSpeed = 1.3f;
    public bool nearPole;
    public bool grabPole;
    FullBodyBipedIK IK;
    public Transform pole;
    Transform grabPointL;
    Transform grabPointR;
    public Transform elbowTargetL;
    public Transform elbowTargetR;

    Transform offsetter;
    float weight = 0;
    public bool isShovelWar;


    // Use this for initialization
    void Start () {
        IK = GetComponent<FullBodyBipedIK>();
        if (isShovelWar == true)
        {

            elbowTargetL = pole.transform.parent.Find("Elbow Direction L");
            elbowTargetR = pole.transform.parent.Find("Elbow Direction R");
            IK.solver.leftArmChain.bendConstraint.bendGoal = elbowTargetL;
            IK.solver.leftArmChain.bendConstraint.weight = 1;
            IK.solver.rightArmChain.bendConstraint.bendGoal = elbowTargetR;
            IK.solver.rightArmChain.bendConstraint.weight = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
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
