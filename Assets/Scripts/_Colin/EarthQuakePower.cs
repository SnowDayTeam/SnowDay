using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RootMotion;

public class EarthQuakePower : MonoBehaviour {

    public CameraShake cameraShake;
    [Range(0.01f, 1)] public float duration;
    [Range(0.01f, 1)] public float magnitude;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            FindObjectOfType<AudioManager>().Play("quake");
            StartCoroutine(cameraShake.ShakeCamera(duration, magnitude));
        }
    }
}
