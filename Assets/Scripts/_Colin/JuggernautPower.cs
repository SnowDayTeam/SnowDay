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

    //private FullBodyBipedIK playerIK;

    // Use this for initialization
    void Start () {
        invulnDuration = maxInvulnDuration;
        juggernautPlayerSphere = transform.GetChild(0);
    }
	
	// Update is called once per frame
	void Update () {
        //should do this part on the player controller probably, set player invuln on trigger enter
        if (isInvuln)
        {
            if (getComp == false)
            {
                //Set the puppet invincible
                puppet.mode = PuppetMaster.Mode.Kinematic;
                playerPos = player.GetComponentInChildren<Animator>().transform;
                juggernautPlayerSphere.localScale = playerPos.localScale;
                getComp = true;
            }
            juggernautPlayerSphere.parent = player.GetComponentInChildren<Animator>().transform;
            //juggernautPlayerSphere.position = player.GetComponentInChildren<Animator>().transform.position;
            juggernautPlayerSphere.position = new Vector3(playerPos.position.x, playerPos.position.y + playerPos.localScale.y/2 + 0.2f, playerPos.position.z);

            invulnDuration -= Time.deltaTime;
            if (invulnDuration <= 0)
            {
                //isInvuln = false;
                Debug.Log("Juggernaut powerup End");
                invulnDuration = maxInvulnDuration;
                puppet.mode = PuppetMaster.Mode.Active;
                Destroy(gameObject);
                Destroy(juggernautPlayerSphere.gameObject);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponentInParent<PlayerController>();
        if (player != null && isInvuln == false)
        {
            puppet = player.GetComponentInChildren<PuppetMaster>();

          //  puppet = other.gameObject.transform.parent.GetComponentInChildren<PuppetMaster>();
            Debug.Log("Juggernaut powerup Start");
            isInvuln = true;
        }
    }
}
