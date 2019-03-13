using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour {

    public Transform cameraPosition;
    public Transform[] cameraPositions;
    public int cameraPositionIndex = 0;
    public bool didHitFirstGate = false;

    public float speed = 0.2F;
    private float startTime;
    private float journeyLength;

    void Start()
    {
       startTime = Time.time;
    }

    void Update()
    {
        if (didHitFirstGate)
        {
            MoveCamera(cameraPositionIndex);
        }
        MoveCamera(cameraPositionIndex);

    }

    public void MoveCamera(int cameraIndex)
    {
        journeyLength = Vector3.Distance(cameraPosition.position, cameraPositions[cameraIndex].position);

        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(cameraPosition.position, cameraPositions[cameraIndex].position, speed);
        transform.rotation = Quaternion.Lerp(cameraPosition.rotation, cameraPositions[cameraIndex].rotation, Time.time * speed);
    }
}
