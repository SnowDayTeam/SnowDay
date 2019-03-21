using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RootMotion.FinalIK;
using RootMotion.Dynamics;

using SnowDay.Diego.CharacterController;

public class EarthQuakePower : MonoBehaviour {

    public CameraShake cameraShake;
    [Range(0.01f, 1)] public float duration;
    [Range(0.01f, 1)] public float magnitude;
    [Range(1, 10)] public float earthQuakeDelay;
    public PuppetMaster[] playerPuppets;
    private PuppetMaster p1;
    private FullBodyBipedIK p1Ik;
    DeathmatchGamemodeManager dm;


    private void Start()
    {
        playerPuppets = FindObjectsOfType<PuppetMaster>();

        dm = FindObjectOfType<DeathmatchGamemodeManager>();
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
                   //FindObjectOfType<AudioManager>().Play("quake");
                   // StartCoroutine(cameraShake.ShakeCamera(duration, magnitude));
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
        PowerUpSpawn.activePowerUpCount--;
        Destroy(gameObject);
    }
  
    private void OnTriggerEnter(Collider other)
    {
        p1 = other.gameObject.GetComponentInParent<PlayerController>().GetComponentInChildren<RootMotion.Dynamics.PuppetMaster>();
        if (p1 != null) {
            print("puppermaster found");
        }

        //will need to change from tag use
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if(player != null)
        {
            p1 = other.gameObject.GetComponentInParent<PuppetMaster>();
            if (p1 != null)
            {
                StartCoroutine(EarthQuakeEffect());
            }
        }

    }

  
}
