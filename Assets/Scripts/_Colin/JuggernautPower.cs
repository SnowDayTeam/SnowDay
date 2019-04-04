using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using RootMotion.FinalIK;
using UnityEngine;
using SnowDay.Diego.CharacterController;

public class JuggernautPower : MonoBehaviour {

    [Range(1, 20)] public float maxInvulnDuration;
    private float invulnDuration = 12;
    private bool isInvuln;
    PuppetMaster puppet;
    public Transform juggernautPlayerSphere;
    Transform playerPos;
    PlayerController player;
    bool getComp;
    Animator animator;
    float waitTime = 1;
    MeshRenderer mesh;


    void Start () {
        invulnDuration = maxInvulnDuration;
        juggernautPlayerSphere = transform.GetChild(0);
        animator = transform.GetChild(0).GetComponent<Animator>();
        animator.speed = maxInvulnDuration/100f;
        mesh = GetComponent<MeshRenderer>();
        print("anim speed " + animator.speed);

    }

    void Update () {

        //--------------Power Up The Player---------------//
        if (isInvuln)
        {
            if (getComp == false)
            {
                animator.SetTrigger("Start");
                //Set the puppet invincible
                puppet.mode = PuppetMaster.Mode.Kinematic;
                playerPos = player.GetComponentInChildren<Animator>().transform;
                juggernautPlayerSphere.localScale = playerPos.localScale;

                getComp = true;
            }

            //Set sphere Position
            if (juggernautPlayerSphere != null)
            {
                if(player== null)
                {
                    Debug.LogError("PLAYRE IS NULL");
                }
               // juggernautPlayerSphere.parent = player.GetComponentInChildren<SnowDayCharacter>().transform ;
                juggernautPlayerSphere.position = new Vector3(playerPos.position.x, playerPos.position.y + playerPos.localScale.y / 2 + 0.2f, playerPos.position.z);
                puppet.mode = PuppetMaster.Mode.Kinematic;


                invulnDuration -= Time.deltaTime;
                if (invulnDuration <= 0)
                {
                    //isInvuln = false;
                    Debug.Log("Juggernaut powerup End");
                    invulnDuration = maxInvulnDuration;
                    Destroy(juggernautPlayerSphere.gameObject);
                    Wait();
                }
            }
        }
	}
    //--------------Powerup Triggered---------------//

    private void OnTriggerEnter(Collider other)
    {
        if(player == null)
        {
            player = other.GetComponentInParent<PlayerController>();

        }
        if (player != null && isInvuln == false)
        {
            puppet = player.GetComponentInChildren<PuppetMaster>();
            puppet.mode = PuppetMaster.Mode.Kinematic;

            Debug.Log("Juggernaut powerup Start " +player.gameObject.name);
            mesh.enabled = false;
            isInvuln = true;
        }
    }

    //--------------Wait and Restore Puppet---------------//

    void Wait()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            print("Reactivate puppet");
            puppet.mode = PuppetMaster.Mode.Active;
            PowerUpSpawn.activePowerUpCount--;
            Destroy(gameObject);
        }
    }
}
