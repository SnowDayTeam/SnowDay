using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
public class StiffArm : MonoBehaviour {

    public bool stiffArming;
    public float reachSpeed = 2;
    float weight = 0;
    FullBodyBipedIK IK;
    Transform handTarget;
    Transform bendGoal;

	void Start () {
        IK = transform.parent.GetComponent<FullBodyBipedIK>();
        handTarget = transform.Find("Hand Target");
        bendGoal = transform.Find("Bend Goal");
        IK.solver.rightHandEffector.target = handTarget;
        IK.solver.rightArmChain.bendConstraint.bendGoal = bendGoal;
        

    }

    void Update () {
		if(stiffArming == true)
        {
            if(weight < 1)
            {
                weight += Time.deltaTime * reachSpeed;
            }
        }
        else
        {
            if (weight > 0)
            {
                weight -= Time.deltaTime * reachSpeed;
            }
        }
        IK.solver.rightHandEffector.positionWeight = weight;
        IK.solver.rightHandEffector.rotationWeight = weight;
        IK.solver.rightArmChain.bendConstraint.weight = weight;



    }
}
