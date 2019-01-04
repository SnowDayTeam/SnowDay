using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class ReachR : MonoBehaviour {

    public FullBodyBipedIK IK;
    public float lerpTime = 1f;
    float currentLerpTime;
    bool lerpOut;
    public bool isIn = false;
    public Transform ball;

    void Update()
    {
        IK.solver.rightHandEffector.target = ball;
        
        if (isIn == false)
        {
            //increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float t = currentLerpTime / lerpTime;
            IK.solver.rightHandEffector.positionWeight = Mathf.Lerp(IK.solver.rightHandEffector.positionWeight, 0.0f, t);
        }

        if (isIn == true)
        {
            //increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            //lerp!
            float t = currentLerpTime / lerpTime;
            IK.solver.rightHandEffector.positionWeight = Mathf.Lerp(IK.solver.rightHandEffector.positionWeight, 1.0f, t);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnowBall")
        {
            currentLerpTime = 0f;

        }

    }
    void OnTriggerStay (Collider other) {
        //print("test");
        if (other.gameObject.tag == "SnowBall"){
            ball = other.transform;
            print("In");
            isIn = true;
            if (other.gameObject == null)
            {
                isIn = false;
                currentLerpTime = 0f;
            }
        }


      

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SnowBall")
        {
            //ball = null;
            print("Out");
            isIn = false;
            currentLerpTime = 0f;
        }
    }

    

}

