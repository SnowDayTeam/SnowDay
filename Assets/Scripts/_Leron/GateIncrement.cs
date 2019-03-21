using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateIncrement : MonoBehaviour
{
    public bool didHit = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!didHit)
            {
                Debug.Log("enteredGate");
                GameObject camera = GameObject.Find("Camera");
                CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
                cameraLerp.cameraPositionIndex++;
                cameraLerp.didHitFirstGate = true;
                didHit = true;
            }
        }
    }

}
