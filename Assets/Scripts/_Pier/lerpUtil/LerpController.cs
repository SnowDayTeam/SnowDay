using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseLerper: MonoBehaviour
{
    public bool isLerping = false;
    public virtual void startLerp()
    {
        isLerping = true;
    }
}
public class LerpController : MonoBehaviour {

    public baseLerper[] lerps;
    PierInputManager input;
    public PierInputManager.ButtonName ping = PierInputManager.ButtonName.Y;
	// Use this for initialization
	void Start () {
        input = gameObject.GetComponentInParent<PierInputManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (input.GetButtonDown(ping))
        {
            startLerp();
        }
	}
    [ContextMenu("lerp")]
    public void startLerp()
    {
        foreach(baseLerper l in lerps)
        {
            l.startLerp();
        }
    }
}
