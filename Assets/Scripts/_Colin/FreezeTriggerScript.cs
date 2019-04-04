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
    Rigidbody[] bones;

    // Use this for initialization
    void Start () {
        isFrozen = false;
        freezeTimer = maxFreezeTime;
	}
	
	// TODO set timer to countdown freeze time and then times up, unfreeze player.
	void Update () {
        if (isFrozen) {
            puppet.mode = PuppetMaster.Mode.Disabled;
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
        frozenPlayerAnimator.transform.SetParent(playerRoot, true);
        // frozenPlayerAnimator.transform.position = new Vector3(frozenPlayerController.GetComponentInChildren<Transform>().transform.position.x * -1, frozenPlayerController.GetComponentInChildren<Transform>().transform.position.y, frozenPlayerController.GetComponentInChildren<Transform>().transform.position.z *-1);
        puppet.mode = PuppetMaster.Mode.Active;
        frozenPlayerAnimator.transform.rotation = new Quaternion(0, 1, 0, 0);
        frozenPlayerAnimator.GetComponent<Rigidbody>().detectCollisions = true;
        frozenPlayerAnimator.GetComponent<Rigidbody>().isKinematic = false;
        frozenPlayerAnimator.GetComponent<Animator>().enabled = true;
        frozenPlayerController.enabled = true;
        puppet.enabled = true;
        puppetBehaviours.enabled = true;
        bones = puppet.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].isKinematic = false;
            bones[i].detectCollisions = true;
            print(bones[i].name);
            if (bones[i].GetComponent<BoxCollider>())
                bones[i].GetComponent<BoxCollider>().enabled = true;
            else if (bones[i].GetComponent<CapsuleCollider>())
            {
                bones[i].GetComponent<CapsuleCollider>().enabled = true;
            }

        }

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
            playerRoot = frozenPlayerController.transform.GetChild(0);
            puppet.mode = PuppetMaster.Mode.Disabled;
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
        print(frozenPlayerController.transform.childCount);
        //disable collision between rigidbodies of player and iceball
        /*   foreach (Transform t in puppet.GetComponentInChildren<Transform>()) {
                if (t.GetComponent<Rigidbody>()) {
                    t.GetComponent<Rigidbody>().detectCollisions = false;
                    t.GetComponent<Rigidbody>().isKinematic = true;
                    t.GetComponentInChildren<Rigidbody>().detectCollisions = false;
                }
           }*/
        //try and check if the bones are colliding
        //disable all bones to kinematic, disable their colliders
        bones = puppet.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < bones.Length; i++) {
            bones[i].isKinematic = true;
            bones[i].detectCollisions = false;
            print(bones[i].name);
            if (bones[i].GetComponent<BoxCollider>())
                bones[i].GetComponent<BoxCollider>().enabled = false;
            else if (bones[i].GetComponent<CapsuleCollider>()) {
                bones[i].GetComponent<CapsuleCollider>().enabled = false;
            }
        }
       
        //set player to kinematic to lock in place inside ball
        frozenPlayerController.GetComponentInChildren<Rigidbody>().detectCollisions = false;
        frozenPlayerController.GetComponentInChildren<Rigidbody>().isKinematic = true;


        //disable puppetmaster animations to prevent weird animation stretching, disabling animator prevents ducking, otherwise character ducks when encased in ice
        frozenPlayerController.GetComponentInChildren<Animator>().enabled = false;
        frozenPlayerController.enabled = false;

        //*********this is not disabling the RB of the bones in code, but manually in the editor setting mode disabled does**********
        puppet.mode = PuppetMaster.Mode.Disabled;

        puppet.enabled = false;
        puppetBehaviours.enabled = false;

        //create ice ball at the players position + a height offset, parent the player to the iceball so it will move and rotate with it.
        //current bug where iceball can spawn infront of player by small amount, no idea why, needs fixing
        iceBallClone = Instantiate(iceBall, frozenPlayerAnimator.transform.position + new Vector3(0, 1, 0), frozenPlayerAnimator.transform.rotation);
        iceBallClone.mass = iceBallClone.mass + frozenPlayerController.GetComponentInChildren<Rigidbody>().mass; //combine mass of ice and player if players are to have differrent weights.
        iceBallClone.transform.parent = playerRoot;
        frozenPlayerAnimator.transform.parent = iceBallClone.transform;
        Debug.Log("Freeze");
    }

  
}
