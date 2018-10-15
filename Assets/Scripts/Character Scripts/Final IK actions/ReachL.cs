using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class ReachL : MonoBehaviour {

    public FullBodyBipedIK IK;
    public float lerpTime = 1f;
    float currentLerpTime;
    bool lerpOut;
    public bool isIn = false;
    public Transform ball;

    void Update()
    {
        IK.solver.leftHandEffector.target = ball;
       

        if (isIn == false)
        {          
            //increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float t = currentLerpTime / lerpTime;
            IK.solver.leftHandEffector.positionWeight = Mathf.Lerp(IK.solver.leftHandEffector.positionWeight, 0.0f, t);
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
            IK.solver.leftHandEffector.positionWeight = Mathf.Lerp(IK.solver.leftHandEffector.positionWeight, 1.0f, t);
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

        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SnowBall")
        {
            //ball = null;
            print("Butts");
            isIn = false;
            currentLerpTime = 0f;

        }
    }

}

