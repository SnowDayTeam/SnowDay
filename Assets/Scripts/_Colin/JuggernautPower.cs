using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using RootMotion.FinalIK;
using UnityEngine;
using SnowDay.Diego.CharacterController;

public class JuggernautPower : MonoBehaviour {

    [Range(1, 10)] public float maxInvulnDuration;
    private float invulnDuration;
    private bool isInvuln;
     PuppetMaster puppet;

    //private FullBodyBipedIK playerIK;

    // Use this for initialization
    void Start () {
        invulnDuration = maxInvulnDuration;
    }
	
	// Update is called once per frame
	void Update () {
        //should do this part on the player controller probably, set player invuln on trigger enter
        if (isInvuln)
        {
            puppet.mode = PuppetMaster.Mode.Kinematic;
            invulnDuration -= Time.deltaTime;
            if (invulnDuration <= 0)
            {
                //isInvuln = false;
                Debug.Log("Juggernaut powerup End");
                invulnDuration = maxInvulnDuration;
                puppet.mode = PuppetMaster.Mode.Active;
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null && isInvuln == false)
        {
            puppet = player.GetComponentInChildren<PuppetMaster>();

          //  puppet = other.gameObject.transform.parent.GetComponentInChildren<PuppetMaster>();
            Debug.Log("Juggernaut powerup Start");
            isInvuln = true;
        }
    }
}
