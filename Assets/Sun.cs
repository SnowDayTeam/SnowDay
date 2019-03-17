using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    float intensity;
    public bool reset;
    public float startIntensity;
    Light light;
	// Use this for initialization
	void Awake ()
    {
        light = GetComponent<Light>();
        intensity = light.intensity;
        startIntensity = intensity;
	}

    private void Update()
    {
        if(reset == true)
        {
            Reset();
        }
    }
    // Update is called once per frame
     void Reset () {
        if (light.intensity <= startIntensity)
        {
            intensity += Time.deltaTime * 1f;
            light.intensity = intensity;
        }
        else
        {
            reset = false;
            light.intensity = startIntensity;
        }
    }
}
