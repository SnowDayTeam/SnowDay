using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;
using SnowDay.Diego.CharacterController;

public class FreezeTriggerScript : MonoBehaviour {

    public Rigidbody iceBall;
    private GameObject frozenPlayer;
    private PlayerController frozenPlayerController;
    private PuppetMaster puppet;
    private BehaviourPuppet puppetBehaviours;
    private GameObject spawnParent;
    [Range(1, 10)] public float maxFreezeTime;
    private float freezeTimer;
    private Animator frozenPlayerAnimator;
    private Vector3 unfrozenTransform;
    private Transform playerRoot;

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
                print("unfreeze");
                UnFreeze();
            }
        }
	}

    //restore players disabled components and reset parent to spawn node, destroy ice ball
    private void UnFreeze() {
        isFrozen = false;
        canFreeze = false;
        iceBallClone.transform.DetachChildren();

        //setting parent back to root causes transform to be incorrect, tried to manual correct but still wrong
        frozenPlayerAnimator.transform.SetParent(frozenPlayerController.GetComponentInChildren<Transform>(), true);
        // frozenPlayerAnimator.transform.position = new Vector3(frozenPlayerController.GetComponentInChildren<Transform>().transform.position.x * -1, frozenPlayerController.GetComponentInChildren<Transform>().transform.position.y, frozenPlayerController.GetComponentInChildren<Transform>().transform.position.z *-1);

        frozenPlayerAnimator.transform.rotation = new Quaternion(0, 1, 0, 0);
        frozenPlayerAnimator.GetComponent<Rigidbody>().detectCollisions = true;
        frozenPlayerAnimator.GetComponent<Rigidbody>().isKinematic = false;
        frozenPlayerAnimator.GetComponent<Animator>().enabled = true;
        frozenPlayerController.enabled = true;
        puppet.enabled = true;
        puppetBehaviours.enabled = true;
        
        Destroy(iceBallClone.gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if gameobject is player...
        if (other.gameObject.tag == "Player") {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            frozenPlayerController = other.GetComponentInParent<PlayerController>();
            puppet = frozenPlayerController.GetComponentInChildren<PuppetMaster>();
            puppetBehaviours = frozenPlayerController.GetComponentInChildren<BehaviourPuppet>();
            frozenPlayerAnimator = frozenPlayerController.GetComponentInChildren<Animator>();
            playerRoot = frozenPlayerAnimator.GetComponentInParent<Transform>();
            if (!canFreeze)
            {
               freezeTimer = maxFreezeTime;
               canFreeze = true;
               FreezePlayer();
            }
        }
    }

    private void FreezePlayer() {
        isFrozen = true;
        //disable collision between rigidbodies of player and iceball
        frozenPlayerController.GetComponentInChildren<Rigidbody>().detectCollisions = false;
        //set player to kinematic to lock in place inside ball
        frozenPlayerController.GetComponentInChildren<Rigidbody>().isKinematic = true;
    

        //disable puppetmaster animations to prevent weird animation stretching, disabling animator prevents ducking, otherwise character ducks when encased in ice
        puppet.mode = PuppetMaster.Mode.Kinematic;
        frozenPlayerController.GetComponentInChildren<Animator>().enabled = false;
        frozenPlayerController.enabled = false;
        puppet.enabled = false;
        puppetBehaviours.enabled = false;

        //create ice ball at the players position + a height offset, parent the player to the iceball so it will move and rotate with it.
        //current bug where iceball can spawn infront of player by small amount, no idea why, needs fixing
        iceBallClone = Instantiate(iceBall, frozenPlayerAnimator.transform.position + new Vector3(0, 1, 0), frozenPlayerAnimator.transform.rotation);
        iceBallClone.mass = iceBallClone.mass + frozenPlayerController.GetComponentInChildren<Rigidbody>().mass; //combine mass of ice and player if players are to have differrent weights.
        frozenPlayerAnimator.transform.SetParent(iceBallClone.transform);
       
        Debug.Log("Freeze");
    }

  
}
