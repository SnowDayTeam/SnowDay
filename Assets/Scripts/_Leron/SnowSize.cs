using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSize : MonoBehaviour {

    public float snowSize = 0.0f;

	
	// Update is called once per frame
	void Update () {

        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * snowSize;

    }
}
