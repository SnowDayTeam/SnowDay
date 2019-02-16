using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpScale : baseLerper
{
    public bool pingPong = false;
    public float lerpDuration = 1f;
    public LerpUtility.lerpMode lerpMode;
    public Vector3 endScale;
    private Vector3 startScale;
    // Use this for initialization
    private bool lerpingdown = false;
    public float currentLerpTime;
	void Start ()
    {
        startScale = transform.localScale;

	}
    
	// Update is called once per frame
	void Update ()
    {
		if(isLerping == true)
        {
            currentLerpTime += Time.deltaTime;

            transform.localScale = Vector3.Lerp(startScale, endScale, LerpUtility.Lerp(currentLerpTime, lerpDuration, lerpMode));
            if(currentLerpTime >= lerpDuration)
            {
                if(pingPong == false)
                {
                    isLerping = false;

                }
                else
                {
                    Vector3 temp = startScale;
                    startScale = endScale;
                    endScale = temp;
                    if (lerpingdown == true)
                    {
                        isLerping = false;
                        lerpingdown = false;
                    }
                    else
                    {
                        lerpingdown = true;

                    }


                }
                currentLerpTime = 0;

            }
        }
	}
}
