using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightningPower : MonoBehaviour {

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


    private void Update()
    {
        if (isActivated == true)
        {
            Timer();
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
            Collider[] colliders = Physics.OverlapSphere(centerPos, radius);
            foreach (Collider hit in colliders)
            {
                if (hit != triggerPlayer.GetComponent<CapsuleCollider>())
                {
                    if (hit.gameObject.name == "BND_Spine2_JNT") { 
                        rb = hit.GetComponent<Rigidbody>();
                        print(rb.name);
                    }

                    if (rb != null && rb != triggerPlayer.GetComponent<Rigidbody>())
                    {                       
                        rb.AddExplosionForce(power, centerPos, radius, 0.1f, ForceMode.Impulse);
                        gameObject.GetComponent<SphereCollider>().enabled = false;
                        gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }           
                PowerUpSpawn.activePowerUpCount--;
                Destroy(gameObject);
    }
}
