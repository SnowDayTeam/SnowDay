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
    Transform offsetter;
    float weight = 0;


    // Use this for initialization
    void Start () {
        IK = GetComponent<FullBodyBipedIK>();
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
            offsetter.forward = IK.transform.forward;

            if(weight < 1)
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
