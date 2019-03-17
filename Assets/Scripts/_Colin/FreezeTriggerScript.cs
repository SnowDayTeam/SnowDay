using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

public class FreezeTriggerScript : MonoBehaviour {

    public Rigidbody iceBall;
    private GameObject frozenPlayer;
    public GameObject puppetMasterIK;
    public GameObject pupperBehaviors;
    private GameObject spawnParent;
    [Range(1, 10)] public float maxFreezeTime;
    private float freezeTimer;
    private GameObject playerRoot;

    private bool isFrozen;
    private bool canFreeze;
    Rigidbody iceBallClone;

    // Use this for initialization
    void Start () {
        isFrozen = false;
        freezeTimer = maxFreezeTime;
	}
	
	// TODO set timer to countdown freeze time and then times up, unfreeze player.
	void Update () {
        if (isFrozen) {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0) {
                UnFreeze();
            }
        }
	}

    //restore players disabled components and reset parent to spawn node, destroy ice ball
    private void UnFreeze() {
        isFrozen = false;
        iceBallClone.transform.DetachChildren();
        frozenPlayer.transform.SetParent(spawnParent.transform);
        frozenPlayer.transform.rotation = new Quaternion(0, 1, 0, 0);
        frozenPlayer.GetComponent<Rigidbody>().detectCollisions = true;
        frozenPlayer.GetComponent<Rigidbody>().isKinematic = false;
        frozenPlayer.GetComponent<Animator>().enabled = true;
        pupperBehaviors.SetActive(true);
        puppetMasterIK.SetActive(true);
        //currently needed to set up on an individual player basis while using unity standard assets player controller.
        frozenPlayer.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
        Destroy(iceBallClone.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        canFreeze = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if gameobject is player...
        if (other.gameObject.tag == "Player") {
            Debug.Log("Freeze123");
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            if (!canFreeze)
            {
               frozenPlayer = other.gameObject;
               playerRoot = frozenPlayer.transform.parent.gameObject;
               spawnParent = playerRoot;
               // puppetMasterIK = other.gameObject.GetComponentInParent<Transform>().GetComponentInChildren<RootMotion.Dynamics.PuppetMaster>();
               freezeTimer = maxFreezeTime;
               canFreeze = true;
               FreezePlayer();
            }
        }
    }

    private void FreezePlayer() {
        isFrozen = true;
        //disable collision between rigidbodies of player and iceball
        frozenPlayer.GetComponent<Rigidbody>().detectCollisions = false;

        //set player to kinematic to lock in place inside ball
        frozenPlayer.GetComponent<Rigidbody>().isKinematic = true;

        //disable puppetmaster animations to prevent weird animation stretching, disabling animator prevents ducking, otherwise character ducks when encased in ice.
        // puppetMasterIK.state = PuppetMaster.State.Frozen;
        puppetMasterIK.SetActive(false);
        pupperBehaviors.SetActive(false);
        frozenPlayer.GetComponent<Animator>().enabled = false;
        //need to update to proper movement script
        frozenPlayer.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;

        //create ice ball at the players position + a height offset, parent the player to the iceball so it will move and rotate with it.
        iceBallClone = Instantiate(iceBall, frozenPlayer.GetComponent<Transform>().transform.position + new Vector3(0, 1.0f, 0), frozenPlayer.GetComponent<Transform>().transform.rotation);
        iceBallClone.mass = iceBallClone.mass + frozenPlayer.GetComponent<Rigidbody>().mass; //combine mass of ice and player if players are to have differrent weights.
        frozenPlayer.GetComponent<Transform>().transform.SetParent(iceBallClone.transform);
        Debug.Log("Freeze");
    }
}
