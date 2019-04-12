using UnityEngine;
using System.Collections.Generic;
using System;
using SnowDay.Diego.CharacterController;

[RequireComponent(typeof(Camera))]
public class DynamicSnowDayCamera : MonoBehaviour {

    [Header("Followed Objects")]
    [Tooltip("The set of players to follow")]
    [SerializeField] GameObject[] PlayersToFollow;
    [Tooltip("Any additional objects that needs to be kept in camera view")]
    [SerializeField] GameObject[] OptionalFollowedObjects;

    [Header("Camera Move Variables")]
    [Tooltip("The time it takes in seconds to end up at the new position from the current position.")]
    [SerializeField] float SmoothDuration = 1.0f;
    [Tooltip("The additional distance to add to currect position.")]
    [SerializeField] Vector3 Offset = Vector3.zero;
    [Header("Anchor Point")]
    [Tooltip("The static object to which player positions are calculated")]
    [SerializeField] Transform AnchorPoint = null;

    [Header("Camera Shake Variables")]
    [Tooltip("The time in seconds that the shake will last, actual time will varry depending on shake move interval")]
    [SerializeField] float ShakeDuration = 0.15f;
    [Tooltip("The 'strength' of the shake, high values move the camera farther.")]
    [SerializeField] float ShakeMagnitude = 0.2f;
    [Tooltip("The time added in between each shake. This will increase the duration of the overall shake.")]
    [Range(0.0f, 0.1f)]
    [SerializeField] float ShakeMoveInterval = 0.013f;

    private bool IsCameraShaking = false;
    private float FarthestDistanceFromAnchor = 0;
    private Vector3 OriginalCameraPosition = Vector3.zero;
    private Vector3 NextCameraPosition = Vector3.zero;

    void Start() {

        if(!this.AnchorPoint) {
            Debug.LogError("Anchor point not set, set a static object anchor point for camera to work");
        }

        this.NextCameraPosition = this.transform.position;
        this.OriginalCameraPosition = this.transform.position;
    }

    private void LateUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            this.StartCoroutine(this.ShakeCamera());
        }
        if(!this.AnchorPoint || this.IsCameraShaking)
            return;

        this.FarthestDistanceFromAnchor = 0;
        this.ZoomCamera();
    }

    public System.Collections.IEnumerator ShakeCamera() 
    {
        Vector3 CurrentPosition = this.transform.position;

        IsCameraShaking = true;
        float TimeElapsed = 0.0f;

        while (TimeElapsed < this.ShakeDuration)
        {
            float RandomShakeX = UnityEngine.Random.Range(-1f, 1f) * this.ShakeMagnitude;
            float RandomShakeY = UnityEngine.Random.Range(-1f, 1f) * this.ShakeMagnitude;

            this.transform.localPosition = new Vector3(CurrentPosition.x + RandomShakeX, 
                        CurrentPosition.y + RandomShakeY, CurrentPosition.z);

            TimeElapsed += Time.deltaTime;

            yield return new WaitForSeconds(this.ShakeMoveInterval);
        }

        IsCameraShaking = false;
    }

    /// <summary>
    /// Zooms camera in and out by move backwards to fit all players in view
    /// </summary>
    private void ZoomCamera() 
    {
        foreach(GameObject FollowedPlayer in this.PlayersToFollow) 
        {
            float CurrentFollowedObjectDistance = Vector3.Distance(this.AnchorPoint.position, 
                                                            FollowedPlayer.transform.position);

            if(CurrentFollowedObjectDistance > FarthestDistanceFromAnchor)
                this.FarthestDistanceFromAnchor = CurrentFollowedObjectDistance;
        }

        foreach(GameObject FollowedObject in this.OptionalFollowedObjects) {

            float CurrentFollowedObjectDistance = Vector3.Distance(this.AnchorPoint.position, 
                                                            FollowedObject.transform.position);

            if(CurrentFollowedObjectDistance > FarthestDistanceFromAnchor)
                this.FarthestDistanceFromAnchor = CurrentFollowedObjectDistance;
        }

        this.NextCameraPosition = (this.OriginalCameraPosition + (-this.transform.forward * this.FarthestDistanceFromAnchor)) + this.Offset;

        this.transform.position = Vector3.Lerp(this.transform.position, this.NextCameraPosition, this.SmoothDuration);
    }
}
