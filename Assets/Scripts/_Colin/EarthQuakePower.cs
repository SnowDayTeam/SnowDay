using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RootMotion.FinalIK;
using RootMotion.Dynamics;

using SnowDay.Diego.CharacterController;

public class EarthQuakePower : MonoBehaviour
{

    private CameraShake cameraShake;
    private Camera gameCamera;
    [Range(0.01f, 1)] public float duration;
    [Range(0.01f, 1)] public float magnitude;
    [Range(1, 10)] public float earthQuakeDelay;
    public PuppetMaster[] playerPuppets;
    private PuppetMaster p1;
    private PlayerController player;
    //  DeathmatchGamemodeManager dm;
    //   int playerIndex = 0;


    private void Start()
    {
        //set default values
        duration = 0.5f;
        magnitude = 1.0f;

        //need reference to all players in game mode to get access to their puppetmaster
        playerPuppets = FindObjectsOfType<PuppetMaster>();

        //camera reference for camera shake
        gameCamera = FindObjectOfType<Camera>();
        cameraShake = gameCamera.GetComponent<CameraShake>();

        //dependant on game mode, would need to get current game mode manager
        /*    dm = FindObjectOfType<DeathmatchGamemodeManager>();
            foreach (PlayerController playerCont in dm.Players) {
                playerPuppets[playerIndex] = GetComponentInChildren<PuppetMaster>();
                playerIndex++;
                print(playerIndex);
            }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.GetComponentInParent<PlayerController>();

            if (player != null)
            {
                p1 = player.GetComponentInChildren<PuppetMaster>();
                if (p1 != null)
                {
                    print("PuppetMaster found");
                    StartCoroutine(EarthQuakeEffect());
                    Camera.main.GetComponent<CameraShake>().StartShake(2, 0.4f);
                }
            }
        }

    }

    //every player except triggering player is knocked down
    IEnumerator EarthQuakeEffect()
    {

        if (p1 != null)
        {
            foreach (PuppetMaster pm in playerPuppets)
            {
                if (pm != p1)
                {
                    if (pm.mode != PuppetMaster.Mode.Kinematic)
                    {
                        pm.state = PuppetMaster.State.Dead;
                    }
                }
            }
            FindObjectOfType<AudioManager>().Play("quake");
            if (cameraShake != null)
            {
                StartCoroutine(cameraShake.ShakeCamera(duration, magnitude));

            }

            //disable trigger and mesh for power up, once delay is over, delete it
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Invoke("Kill", earthQuakeDelay + 1);
        }

        yield return new WaitForSeconds(earthQuakeDelay);


        foreach (PuppetMaster pm in playerPuppets)
        {
            if (pm != p1)
            {
                pm.state = PuppetMaster.State.Alive;
            }
        }
        yield return null;
    }

    void Kill()
    {
        PowerUpSpawn.activePowerUpCount--;
        Destroy(gameObject);
    }


}