using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelLerpController : MonoBehaviour {

    [Header("Positions")]
    public Transform drawbackPos;
    public Transform groundPos;
    public Transform throwPos;
    public Transform neutralPos;

     lerpPosition lerpPosition;
     lerpRotation lerpRotation;

    [Header("Bools")]
    public bool shovelling;
    public bool throwing;

    [Header("Timing")]
    public float pullBackTime = 0.5f;
    public float downTime = 0.35f;
    public float neutralTime = 0.5f;
    public float throwTime = 0.5f;
    public float throwPullBackTime = 0.75f;
    public float throwReturnTime = 0.5f;

    public bool canGetStartPos = true; //can go to drawBack pos
    public bool canGetStartPos2 = true; //can go to ground pos
    public bool canGetStartPos3 = true; //can return to neutral
    public bool canGetStartPos4 = true; //can throw
    public bool canThrow;
    bool hasThrown;



    private void Start()
    {
        lerpPosition = GetComponent<lerpPosition>();
        lerpRotation = GetComponent<lerpRotation>();
    }
	
	// Update is called once per frame
	void Update () {

        //---------------------------------Shovelling----------------------------//

		if (shovelling == true && throwing == false)
        {

            //Get Start Pos //Lerp Back
            if (canGetStartPos == true)
            {
                canGetStartPos3 = true;

                //Position
                lerpPosition.currentLerpTime = 0;
                lerpPosition.startPositon = transform.localPosition;
                lerpPosition.endPosition = drawbackPos.localPosition;
                lerpPosition.lerpDuration = pullBackTime;
                lerpPosition.isLerping = true;

                //Rotation
                lerpRotation.currentLerpTime = 0;
                lerpRotation.startRotation = transform.localRotation;
                lerpRotation.endRotation = drawbackPos.localRotation;
                lerpRotation.lerpDuration = pullBackTime;
                lerpRotation.isLerping = true;

                canGetStartPos = false;
            }

            //Once it's back, lerp to ground pos
            if (Vector3.Distance(transform.localPosition, drawbackPos.localPosition) < 0.1f && throwing == false)
            {
                if (canGetStartPos2 == true)
                {
                    //Position
                    lerpPosition.currentLerpTime = 0;
                    lerpPosition.isLerping = false;
                    lerpPosition.startPositon = transform.localPosition;
                    lerpPosition.endPosition = groundPos.localPosition;
                    lerpPosition.lerpDuration = downTime;
                    lerpPosition.isLerping = true;

                    //Rotation
                    lerpRotation.currentLerpTime = 0;
                    lerpRotation.isLerping = false;
                    lerpRotation.startRotation = transform.localRotation;
                    lerpRotation.endRotation = groundPos.localRotation;
                    lerpRotation.lerpDuration = downTime;
                    lerpRotation.isLerping = true;


                    canGetStartPos2 = false;
                }
            }

            if (Vector3.Distance(transform.localPosition, groundPos.localPosition) < 0.1f)
            {
                lerpPosition.isLerping = false;
            }
        }


        //Return Shovel to neutral position if we're not shovelling or throwing. 
        if (shovelling == false && throwing == false)
        {
            if (Vector3.Distance(transform.localPosition, neutralPos.localPosition) > 0.1f)
            {
                if (canGetStartPos3 == true)
                {
                    //Positions
                    lerpPosition.startPositon = transform.localPosition;
                    lerpPosition.endPosition = neutralPos.transform.localPosition;
                    lerpPosition.currentLerpTime = 0;
                    lerpPosition.isLerping = true;

                    //Rotations
                    lerpRotation.startRotation = transform.localRotation;
                    lerpRotation.endRotation = neutralPos.transform.localRotation;
                    lerpRotation.currentLerpTime = 0;
                    lerpRotation.isLerping = true;

                    //Are we retuning from shovelling or throwing?
                    if (hasThrown == true)
                    {
                        lerpPosition.lerpDuration = throwPullBackTime;
                    }
                    else
                    {
                        lerpPosition.lerpDuration = pullBackTime;
                    }

                    canGetStartPos3 = false;
                }
            }
            else
            {
                hasThrown = false;
                canGetStartPos = true;
                canGetStartPos2 = true;
                canGetStartPos4 = true;
                canThrow = true;

                lerpPosition.isLerping = false;
            }
        }

        //---------------------------------Throwing----------------------------//

        if (canThrow == true)
        {
            if (throwing == true)
            {

                //Get Start Pos //Lerp Back
                if (canGetStartPos == true)
                {
                    canGetStartPos3 = true;
                    print("draw back to throw");

                    //Position
                    lerpPosition.currentLerpTime = 0;
                    lerpPosition.startPositon = transform.localPosition;
                    lerpPosition.endPosition = drawbackPos.localPosition;
                    lerpPosition.lerpDuration = pullBackTime;
                    lerpPosition.isLerping = true;

                    //Rotation
                    lerpRotation.currentLerpTime = 0;
                    lerpRotation.startRotation = transform.localRotation;
                    lerpRotation.endRotation = drawbackPos.localRotation;
                    lerpRotation.lerpDuration = pullBackTime;
                    lerpRotation.isLerping = true;

                    canGetStartPos = false;
                }

                //Once it's back, lerp to throw pos
                if (Vector3.Distance(transform.localPosition, drawbackPos.localPosition) < 0.1f && throwing == true)
                {
                    if (canGetStartPos4 == true)
                    {
                        print("throwing");
                        //Position
                        lerpPosition.currentLerpTime = 0;
                        lerpPosition.isLerping = false;
                        lerpPosition.startPositon = transform.localPosition;
                        lerpPosition.endPosition = throwPos.localPosition;
                        lerpPosition.lerpDuration = throwTime;
                        lerpPosition.isLerping = true;

                        //Rotation
                        lerpRotation.currentLerpTime = 0;
                        lerpRotation.isLerping = false;
                        lerpRotation.startRotation = transform.localRotation;
                        lerpRotation.endRotation = throwPos.localRotation;
                        lerpRotation.lerpDuration = throwTime;
                        lerpRotation.isLerping = true;

                        canGetStartPos4 = false;


                    }
                }
                if (Vector3.Distance(transform.localPosition, throwPos.localPosition) < 0.1f)
                {
                    print("Reached Throw Point");

                    shovelling = false;
                    canThrow = false;
                    throwing = false;
                    hasThrown = true;
                }

            }
        }
    }
}
