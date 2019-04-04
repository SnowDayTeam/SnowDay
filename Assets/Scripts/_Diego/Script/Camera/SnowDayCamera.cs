using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using SnowDay.Diego.CharacterController;

public class SnowDayCamera : MonoBehaviour
{
    [Header("Players")]
    public List<PlayerController> Players;

    [Header("Non-Player Objects")]
    public List<GameObject> NPOtoTrack;

    [Header("Camera Horizontal Rotation")]
    public int Resolution = 20;
    public int CurrentStep = 1;

    public float CameraYOffset = 10;
    public float Radius = 10;

    //Time to Destination
    public float SmoothTime = .5f;

    //Camera Movement Multipliers
    public float XZMultiplier = 1f;
    public float YMultiplier = 0.5f;

    //Border Buffer
    public float EdgeBorderBuffer = 8;

    private Camera cam;

    private float thetaStep;
    private Vector3 camVelocity;

    private Vector3 nextCameraPos;

    private bool cameraActive = false;

    public bool CameraShaking = false;

    void Start() {
        
    }

    public void Initialize()
    {
        cam = GetComponentInChildren<Camera>();
        thetaStep = (2f * Mathf.PI) / Resolution;

        CameraDistance();
        PopCamera();

        cameraActive = true;
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            this.StartCoroutine(this.CameraShake(0.25f, 0.15f));
        }

        CameraDistance();
        FindNextCameraPosition();
        if(cam != null)
        {
            if (Vector3.Distance(cam.transform.position, nextCameraPos) > 0.1f && !CameraShaking)
            {
                //Debug.Log("Moved Camera");
                MoveCamera();
            }
        }
       
    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 startPos = transform.localPosition;

        CameraShaking = true;
        float timeElapsed = 0.0f;

        Debug.Log("Shake");
        while (timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x + x, cam.transform.localPosition.y + y, cam.transform.localPosition.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        CameraShaking = false;
    }

    public void SetTargetPlayers(List<PlayerController> players)
    {
        if (players != null)
        {
            Players = players;
        }
    }

    public List<PlayerController> GetTargetPlayers()
    {
        return Players;
    }

    /// <summary>
    /// Sets the Radius and the Y Offset of the Camera,
    /// based on the Greatest distance from the center of of the camera Gimbel.
    /// </summary>
    private void CameraDistance()
    {
        float greatestDistance = 0;
        for (int i = 0; i < Players.Count; i++)
        {
            if (!Players[i].gameObject.activeSelf)
                continue;

            float distanceBetween = Vector3.Distance(transform.position, Players[i].GetCharacterPosition());
            if (distanceBetween > greatestDistance)
            {
                greatestDistance = distanceBetween;
            }
        }

        Radius = greatestDistance + EdgeBorderBuffer * XZMultiplier;
        CameraYOffset = greatestDistance + EdgeBorderBuffer * YMultiplier;
    }

    /// <summary>
    /// Finds the Next Position for the Camera;
    /// </summary>
    void FindNextCameraPosition() {
        transform.position = FindAveragePosition();
        nextCameraPos = transform.position + new Vector3(Radius * Mathf.Cos(thetaStep * CurrentStep), CameraYOffset, Radius * Mathf.Sin(thetaStep * CurrentStep));//Radius * Mathf.Sin(thetaStep * CurrentStep));
    }

    /// <summary>
    /// Instantly Moves Camera to Current Rotation Step
    /// </summary>
    void PopCamera()
    {
        cam.transform.position = nextCameraPos;
        cam.transform.LookAt(transform.position, Vector3.up);
    }

    /// <summary>
    /// Smoothly moves the camera between rotation steps.
    /// </summary>
    private void MoveCamera()
    {
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, nextCameraPos, ref camVelocity, SmoothTime);
        cam.transform.LookAt(transform.position, Vector3.up);
    }

    /// <summary>
    /// Finds the Average Position between all the players
    /// </summary>
    /// <returns>Center Position between the players</returns>
    private Vector3 FindAveragePosition()
    {
        if (Players == null )
            return Vector3.zero;

        Vector3 averagePosition = new Vector3();
        int numofPlayers = 0;
        for (int i = 0; i < Players.Count; i++)
        {
            if (!Players[i].gameObject.activeSelf)
                continue;

            averagePosition += Players[i].GetCharacterPosition();
            numofPlayers++;
        }
        for (int i = 0; i < NPOtoTrack.Count; i++)
        {
            if (!NPOtoTrack[i].activeSelf)
                continue;

            averagePosition += NPOtoTrack[i].transform.position;
            numofPlayers++;
        }
        if( numofPlayers == 0)
        {
            return Vector3.zero;
        }
        averagePosition = averagePosition / numofPlayers;
        averagePosition.y = transform.position.y;
        return averagePosition;
    }

    private void OnDrawGizmos()
    {
        Vector3? AveragePosition = null;
        float? theta = 0;

        Gizmos.color = Color.green;
        AveragePosition = FindAveragePosition();
        Gizmos.color = Color.red;
        theta = 0;
        float thetaStep = (2f * Mathf.PI) / Resolution;
    
        Gizmos.DrawWireSphere(AveragePosition.GetValueOrDefault(), 1);
        for (int i = 0; i < Resolution; i++)
        {
            Vector3 pos = transform.position + new Vector3(Radius * Mathf.Cos(theta.GetValueOrDefault()), CameraYOffset, Radius * Mathf.Sin(theta.GetValueOrDefault()));
            Gizmos.DrawWireSphere(pos, 1);
            theta += thetaStep;
        }
        
    }
}
