using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTriggerScript : MonoBehaviour {

    public Rigidbody iceBall;
    public GameObject puppetMasterIK;
    public GameObject pupperBehaviors;
    public GameObject spawnParent;
    [Range(1, 10)] public float maxFreezeTime;
    private float freezeTimer;

    private bool isFrozen;
    private bool canFreeze;
    private Rigidbody rb;
    Rigidbody iceBallClone;

    // Use this for initialization
    void Start () {
        isFrozen = false;
        rb = GetComponent<Rigidbody>();
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
        transform.SetParent(spawnParent.transform);
        transform.rotation = new Quaternion(0, 1, 0, 0);
        rb.detectCollisions = true;
        rb.isKinematic = false;
        pupperBehaviors.SetActive(true);
        puppetMasterIK.SetActive(true);
        //currently needed to set up on an individual player basis while using unity standard assets player controller.
        gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
        Destroy(iceBallClone.gameObject);
        Debug.Log("Thaw");
    }
    private void OnTriggerExit(Collider other)
    {
        canFreeze = false;
        Debug.Log("CanFreeze: " + canFreeze);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if gameobject tag is icetrigger event...
        if (other.gameObject.tag == "Snow") {
            if (!canFreeze)
            {
                freezeTimer = maxFreezeTime;
                canFreeze = true;
                FreezePlayer();
                Debug.Log("CanFreeze: " + canFreeze);
            }
        }
    }

    private void FreezePlayer() {
        isFrozen = true;
        //disable collision between rigidbodies of player and iceball
        rb.detectCollisions = false;
        //set player to kinematic to lock in place inside ball
        rb.isKinematic = true;
        //disable puppetmaster animations to prevent weird animation stretching, disabling animator prevents ducking, otherwise character ducks when encased in ice.
        puppetMasterIK.SetActive(false);
        pupperBehaviors.SetActive(false);
        GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;

        //create ice ball at the players position + a height offset, parent the player to the iceball so it will move and rotate with it.
        iceBallClone = Instantiate(iceBall, transform.position + new Vector3(0, 1.0f, 0), transform.rotation);
        iceBallClone.mass = iceBallClone.mass + rb.mass; //combine mass of ice and player if players are to have differrent weights.
        transform.SetParent(iceBallClone.transform);
        //TODO disable player movement
        Debug.Log("Freeze");
    }
}
