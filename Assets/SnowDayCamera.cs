using System.Collections.Generic;
using UnityEngine;

public class SnowDayCamera : MonoBehaviour
{
    [Header("Players")]
    public List<Transform> Players;

    private Camera cam;

    [Header("Camera Horizontal Rotation")]
    [Range(0, 10)]
    public int CurrentStep = 1;

    public float SmoothTime = .5f;

    public float CameraYOffset = 10;
    public float Radius = 10;
    public int Resolution = 10;

    public float EdgeBorderBuffer = 5;

    public float XZLimister;
    public float YLimiter;

    private float thetaStep;
    private Vector3 camVelocity;

    private Vector3 nextCameraPos;

    // Use this for initialization
    private void Start()
    {
        cam = Camera.main;
        thetaStep = (2f * Mathf.PI) / Resolution;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        MoveCamera();
        CameraDistance();
    }

    private void CameraDistance()
    {
        float greatestDistance = 0;
        for (int i = 0; i < Players.Count; i++)
        {
            if (!Players[i].gameObject.activeSelf)
                continue;

            float distanceBetween = Vector3.Distance(transform.position, Players[i].position);
            if (distanceBetween > greatestDistance)
            {
                greatestDistance = distanceBetween;
            }
        }

        Radius = greatestDistance  + EdgeBorderBuffer / XZLimister;
        CameraYOffset = greatestDistance  + EdgeBorderBuffer / YLimiter;
        
    }

    private void MoveCamera()
    {
        transform.position = FindAveragePosition();
        nextCameraPos = transform.position + new Vector3(Radius * Mathf.Cos(thetaStep * CurrentStep), CameraYOffset, Radius * Mathf.Sin(thetaStep * CurrentStep));//Radius * Mathf.Sin(thetaStep * CurrentStep));
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, nextCameraPos, ref camVelocity, SmoothTime);
        cam.transform.LookAt(transform.position, Vector3.up);
    }

    private Vector3 FindAveragePosition()
    {
        Vector3 averagePosition = new Vector3();
        int numofPlayers = 0;
        for (int i = 0; i < Players.Count; i++)
        {
            if (!Players[i].gameObject.activeSelf)
                continue;

            averagePosition += Players[i].position;
            numofPlayers++;
        }
        averagePosition = averagePosition / numofPlayers;
        averagePosition.y = transform.position.y;
        return averagePosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 AveragePosition = FindAveragePosition();
        Gizmos.DrawWireSphere(AveragePosition, 1);
        Gizmos.color = Color.red;
        float theta = 0;
        float thetaStep = (2f * Mathf.PI) / Resolution;

        for (int i = 0; i < Resolution; i++)
        {
            Vector3 pos = transform.position + new Vector3(Radius * Mathf.Cos(theta), CameraYOffset, Radius * Mathf.Sin(theta));
            Gizmos.DrawWireSphere(pos, 1);
            theta += thetaStep;
        }
    }
}