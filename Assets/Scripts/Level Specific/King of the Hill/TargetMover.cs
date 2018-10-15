using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TargetMover : MonoBehaviour
{
    public bool isActive;



    void FixedUpdate()
    {

        if (isActive == true)
        {
            //assuming we're only using the single camera:
            var camera = Camera.main;

            //Data recieved from CrossPlatformInput Handler:
            float horizontalAxis = Input.GetAxisRaw("Horizontal Right Stick");
            float verticalAxis = Input.GetAxisRaw("Vertical Right Stick");
            print("v " + verticalAxis + "h " + horizontalAxis);

            float facing = camera.transform.rotation.y;
            float DistanceFromNeutral = 0;
            float transformZ = 0;
            float transformX = 0;
            float finalZ = 0;
            float finalX = 0;

            if (facing > -90 && facing <= 90)
            { //facing forward
                if (facing >= 0)
                {
                    DistanceFromNeutral = (90 - facing);
                }
                else
                {
                    if (facing < 0)
                    {
                        DistanceFromNeutral = (90 + facing);
                    };
                };


                transformX = (1 / 90) * (DistanceFromNeutral);
                transformZ = 90 - transformX;

                transform.position = new Vector3(transformX, transform.position.y, transformZ);
            }
        }
    }
}
           