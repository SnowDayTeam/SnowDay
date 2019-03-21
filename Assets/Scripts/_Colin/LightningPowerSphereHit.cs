using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RootMotion.Dynamics;

public class LightningPowerSphereHit : MonoBehaviour {

    public bool isActivated;
    [HideInInspector]
    public Vector3 centerPos;
    [Range(10, 100)]
    public float radius;
    [Range(5, 2000)]
    public float power;
    Rigidbody rb;
    public float timer = 2f;
    private GameObject triggerPlayer;
    lerpPosition lerpPosScript;
    Collider blastCollider;
    public PuppetMaster playerPuppet;
    bool isTriggered;
    bool once;
    float timer2 = 1f;
    MeshRenderer mesh;
    public Light sun;
    public Light godRay;
    float lightStartIntensity;
    public float sunLowIntensity = 0.2f;
    public float sunDownSpeed = 1f;
    public bool lightsOut;
    GameObject sunObject;

    private void Start()
    {
        lerpPosScript = transform.GetChild(0).GetComponent<lerpPosition>();
        blastCollider = transform.GetChild(0).GetComponent<SphereCollider>();
        mesh = transform.GetChild(1).GetComponent<MeshRenderer>();
        sunObject = GameObject.FindGameObjectWithTag("Sun");
        sun = sunObject.GetComponent<Light>();
        lightStartIntensity = sunObject.GetComponent<Sun>().startIntensity;
        godRay = transform.GetChild(2).GetComponent<Light>();
        godRay.transform.parent = null;
    }


    //--------------Powerup Triggered---------------//
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (isTriggered == false)
            {
                //lightStartIntensity = sun.gameObject.GetComponent<Sun>().startIntensity;
                print("Timer starts");
                //Debug.Log("HIT");
                // FindObjectOfType<AudioManager>().Play("lightning");
                triggerPlayer = other.gameObject;
                centerPos = transform.position;
                isActivated = true;
                mesh.enabled = false;
                isTriggered = true;
                // transform.GetChild(1)
            }
        }
    }
    private void Update()
    {
        //called from onTrigger
        if (isActivated == true)
        {
            Timer();
        }
        if(lightsOut == true)
        {
            LightsOut();
        }

    }
    //--------------Count Down to Lighning Strike---------------//
    void Timer()
    {
        if (once == false)
        {
            //Get the puppet and have the triggering player ignore the bolt
            playerPuppet = triggerPlayer.transform.parent.GetComponentInChildren<PuppetMaster>();
            playerPuppet.mode = PuppetMaster.Mode.Kinematic;
            once = true;
        }
        //Start the timer
        timer -= Time.deltaTime;
        //Lights Out
        LightsOut();
        if (timer <= 0)
        {
            LightningStrike();
        }
    }

    //--------------Lightning Strike---------------//

    private void LightningStrike()
    {
        //Lerp up the ball 
        lerpPosScript.endPosition = transform.position;
        lerpPosScript.isLerping = true;

        if ((lerpPosScript != null && Vector3.Distance(lerpPosScript.transform.position, lerpPosScript.endPosition) < 0.1f))
        {
            //When the ball is lerped, wait a second and delete object and re enable puppet
            Destroy(blastCollider.gameObject);
            LightsUp();
            isActivated = false;
            Wait();
        }
    }

    //------------------Lighting Stuff--------------------//
    void LightsOut()
    {
        //fade out sun
        if (sun.intensity > sunLowIntensity)
        {
            print("sundown");
            sun.intensity -= Time.deltaTime * sunDownSpeed;
        }
        if (godRay != null)
        {
            if (godRay.intensity < 14)
            {
                print("spotlight up");

                godRay.intensity += Time.deltaTime * sunDownSpeed;
            }
            if (godRay.spotAngle > 10)
            {
                print("spotlight up");

                godRay.spotAngle -= Time.deltaTime * sunDownSpeed * 20;
            }
        }
    }

    void LightsUp()
    {
        sun.GetComponent<Sun>().reset = true;
    }


    //--------------Restore Player Settings and Finish---------------//

    private void Wait()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            playerPuppet = triggerPlayer.transform.parent.GetComponentInChildren<PuppetMaster>();
            playerPuppet.mode = PuppetMaster.Mode.Active;
            print("Destroyed lightning");
            PowerUpSpawn.activePowerUpCount--;
            Destroy(gameObject);
        }
    }

}


//OLD SHIT

 //if (hit != triggerPlayer.GetComponent<CapsuleCollider>())
 //           {
 //               if (hit.gameObject.name == "BND_Spine2_JNT")
 //               {
 //                   rb = hit.GetComponent<Rigidbody>();
 //                   print(rb.name);
 //               }

 //               if (rb != null && rb != triggerPlayer.GetComponent<Rigidbody>())
 //               {
 //                   rb.AddExplosionForce(power, centerPos, radius, 0.1f, ForceMode.Impulse);
 //                   gameObject.GetComponent<SphereCollider>().enabled = false;
 //                   gameObject.GetComponent<MeshRenderer>().enabled = false;
 //               }
 //           }
