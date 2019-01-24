using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EarthQuakePower1 : MonoBehaviour {

    public SnowDayCamera cameraScript;
    [Range(0.01f, 1)] public float duration;
    [Range(0.01f, 1)] public float magnitude;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            //FindObjectOfType<AudioManager>().Play("quake");
            StartCoroutine(cameraScript.CameraShake(duration, magnitude));
        }
    }
}
