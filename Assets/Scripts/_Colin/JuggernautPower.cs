using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using RootMotion.FinalIK;
using UnityEngine;

public class JuggernautPower : MonoBehaviour {

    [Range(1, 10)] public float maxInvulnDuration;
    private float invulnDuration;
    private bool isInvuln;
    private PuppetMaster p1;
    private FullBodyBipedIK playerIK;

    // Use this for initialization
    void Start () {
        invulnDuration = maxInvulnDuration;
    }
	
	// Update is called once per frame
	void Update () {
        //should do this part on the player controller probably, set player invuln on trigger enter
        if (isInvuln) {
            p1.mode = PuppetMaster.Mode.Kinematic;
            invulnDuration -= Time.deltaTime;
            if (invulnDuration <= 0) {
                isInvuln = false;
                Debug.Log(isInvuln);
                invulnDuration = maxInvulnDuration;
                p1.mode = PuppetMaster.Mode.Active;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            playerIK = other.gameObject.GetComponentInParent<Transform>().GetComponentInChildren<FullBodyBipedIK>();
           // p1 = other.gameObject.GetComponentInParent<Transform>().GetComponentInChildren<PuppetMaster>();
            Debug.Log(p1);
           // other.gameObject.GetComponent<PlayerController>().setInvuln = true;
            isInvuln = true;
        }
    }
}
