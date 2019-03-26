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

    //-----------------------------
    private AudioManager audioManager;
    private bool onetime = false;

    //-----------------------------



    // Use this for initialization
    void Start () {

        input = gameObject.GetComponentInParent<PierInputManager>();

        //-----------------------------
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        //-----------------------------


    }

    // Update is called once per frame
    void Update () {

        if (input.GetButtonDown(ping))
        {
            if (!onetime)
            {
                if(audioManager != null)
                {
                    audioManager.Play("UPGRADE");

                }
                startLerp();

                onetime = true;
            }      
        }

        if (input.GetButtonUp(ping))
        {
           
          onetime = false;
           
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
