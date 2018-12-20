using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnowSize : MonoBehaviour {

    private float snowSizePercent = 0.0f;
    public Vector3 maxSnowSize = new Vector3(.5f, .5f, .5f);
	
	// Update is called once per frame


    public void setSnowPercent(float val)
    {
        snowSizePercent = val;
        gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, maxSnowSize, snowSizePercent);
        

    }
}
