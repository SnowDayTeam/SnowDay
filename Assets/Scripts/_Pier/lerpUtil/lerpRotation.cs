using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpRotation : baseLerper
{

    public bool pingPong = false;
    public float lerpDuration = 1f;
    public LerpUtility.lerpMode lerpMode;
    public Vector3 Rotation;

    private Quaternion endRotation;
    private Quaternion startRotation;
    // Use this for initialization
    private bool lerpingdown = false;
    public float currentLerpTime;
    void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(Rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLerping == true)
        {
            currentLerpTime += Time.deltaTime;

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, LerpUtility.Lerp(currentLerpTime, lerpDuration, lerpMode));
            if (currentLerpTime >= lerpDuration)
            {
                if (pingPong == false)
                {
                    isLerping = false;

                }
                else
                {
                    Quaternion temp = startRotation;
                    startRotation = endRotation;
                    endRotation = temp;
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
