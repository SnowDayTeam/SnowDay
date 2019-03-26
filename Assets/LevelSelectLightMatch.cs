using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectLightMatch : MonoBehaviour {

    public Light mainLight;
    Light thisLight;
    public float percentage = 10;

    void Start () {
        thisLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        thisLight.intensity = mainLight.intensity / percentage;
	}
}
