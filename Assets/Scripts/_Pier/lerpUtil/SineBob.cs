using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBob : MonoBehaviour
{
    Vector3 start;
    public Vector3 Dir;
    public float Amplitude = 0.2f;
    public float Speed = 1f;
    public bool Local;
    // Use this for initialization
    void Start ()
    {
        if (Local)
            start = transform.localPosition;
        else
            start = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Local)
        {
            transform.localPosition = LerpUtility.SineWave(Time.time, start, (Dir), Amplitude, Speed);

        }
        else
        {
            transform.position = LerpUtility.SineWave(Time.time, start, Dir, Amplitude, Speed);

        }

    }
}
