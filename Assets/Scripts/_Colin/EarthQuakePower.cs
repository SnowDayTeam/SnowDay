﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RootMotion.Dynamics;

public class EarthQuakePower : MonoBehaviour {

    public CameraShake cameraShake;
    [Range(0.01f, 1)] public float duration;
    [Range(0.01f, 1)] public float magnitude;
    [Range(1, 10)] public float earthQuakeDelay;
    public List<PuppetMaster> playerPuppets;
    private RootMotion.Dynamics.PuppetMaster p1;
  
    private void Start()
    {
        
    }

    //every player except triggering player is knocked down
    IEnumerator EarthQuakeEffect() {
        print("DIE");
        if (p1 != null)
        {
            foreach (PuppetMaster pm in playerPuppets)
            {
                if (pm != p1)
                {
                    FindObjectOfType<AudioManager>().Play("quake");
                    StartCoroutine(cameraShake.ShakeCamera(duration, magnitude));
                    pm.state = PuppetMaster.State.Dead;
                }
            }
             //disable trigger and mesh for power up, once delay is over, delete it
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Invoke("Kill", earthQuakeDelay+1);
        }
     
        yield return new WaitForSeconds(earthQuakeDelay);
        print("live");
     
        foreach (PuppetMaster pm in playerPuppets)
        {
            if (pm != p1)
            {
                pm.state = PuppetMaster.State.Alive;
            }
        }
        yield return null;
    }

    void Kill() {
        Destroy(gameObject);
    }
  
    private void OnTriggerEnter(Collider other)
    {        
        //will need to change from tag use
        if (other.gameObject.tag != "SnowBall") {
            p1 = other.gameObject.GetComponentInParent<RootMotion.Dynamics.PuppetMaster>();
            if (p1 != null)
            {
                StartCoroutine(EarthQuakeEffect());
            }
        }
     
    }

  
}
