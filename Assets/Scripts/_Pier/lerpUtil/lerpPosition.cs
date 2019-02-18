using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpPosition : baseLerper
{

    public bool pingPong = false;
    public float lerpDuration = 1f;
    public LerpUtility.lerpMode lerpMode;
    public Vector3 endPosition;
    private Vector3 startPositon;
    // Use this for initialization
    public bool local = false;
    private bool lerpingdown = false;
    private float currentLerpTime;
    void Start()
    {
        if (local)
        {
            startPositon = transform.localPosition;
        }
        else
        {
            startPositon = transform.position;

        }

    }
    public override void startLerp()
    {
        base.startLerp();

    }
    // Update is called once per frame
    void Update()
    {
        if (isLerping == true)
        {
            currentLerpTime += Time.deltaTime;
            if (local)
            {
                transform.localPosition = Vector3.Lerp(startPositon, endPosition, LerpUtility.Lerp(currentLerpTime, lerpDuration, lerpMode));

            }
            else
            {
                transform.position = Vector3.Lerp(startPositon, endPosition, LerpUtility.Lerp(currentLerpTime, lerpDuration, lerpMode));

            }
            if (currentLerpTime >= lerpDuration)
            {
                if (pingPong == false)
                {
                    isLerping = false;

                }
                else
                {
                    Vector3 temp = startPositon;
                    startPositon = endPosition;
                    endPosition = temp;
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
