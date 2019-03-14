using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RootMotion.Dynamics;

public class LightningPowerSphereHit : MonoBehaviour {

    public bool isActivated;
    [HideInInspector]
    public Vector3 centerPos;
    [Range(10, 100)]
    public float radius;
    [Range(5, 2000)]
    public float power;
    Rigidbody rb;
    float timer= 2f;
    private GameObject triggerPlayer;
    lerpPosition lerpPosScript;
    Collider blastCollider;

    private void Start()
    {
        lerpPosScript = transform.GetChild(0).GetComponent<lerpPosition>();
        blastCollider = transform.GetChild(0).GetComponent<SphereCollider>();
    }
    private void Update()
    {
        //called from onTrigger
        if (isActivated == true)
        {
            Timer();
        }
        //kill powerup after lerp
        if (lerpPosScript.transform.position == lerpPosScript.endPosition)
        {
            Destroy(gameObject);
            print("Killed lightning powerup");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            //Debug.Log("HIT");
           // FindObjectOfType<AudioManager>().Play("lightning");
            triggerPlayer = other.gameObject;
            centerPos = transform.position;
            isActivated = true;
            print("Timer starts");
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(centerPos, radius);
    }
    void Timer()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            LightningStrike();
            isActivated = false;
            print("Timer Done");
        }
    }

    private void LightningStrike()
    {
      //Ignore the triggering player
        Collider[] playerPuppet = triggerPlayer.transform.parent.GetComponentInChildren<PuppetMaster>().GetComponentsInChildren<CapsuleCollider>();
        print(playerPuppet.Length + " Bones");
        foreach (CapsuleCollider bone in playerPuppet)
        {
            Physics.IgnoreCollision(bone, blastCollider);
        }
            
      
    //Lerp up the ball 
        lerpPosScript.endPosition = transform.position;
        lerpPosScript.isLerping = true;
        PowerUpSpawn.activePowerUpCount--;
    }
}


//OLD SHIT

 //if (hit != triggerPlayer.GetComponent<CapsuleCollider>())
 //           {
 //               if (hit.gameObject.name == "BND_Spine2_JNT")
 //               {
 //                   rb = hit.GetComponent<Rigidbody>();
 //                   print(rb.name);
 //               }

 //               if (rb != null && rb != triggerPlayer.GetComponent<Rigidbody>())
 //               {
 //                   rb.AddExplosionForce(power, centerPos, radius, 0.1f, ForceMode.Impulse);
 //                   gameObject.GetComponent<SphereCollider>().enabled = false;
 //                   gameObject.GetComponent<MeshRenderer>().enabled = false;
 //               }
 //           }
