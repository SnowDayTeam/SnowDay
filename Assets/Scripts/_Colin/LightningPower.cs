using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightningPower : MonoBehaviour {

    public bool isActivated;
    public Vector3 centerPos;
    [Range(10, 100)]
    public float radius;
    [Range(5, 2000)]
    public float power;
    Rigidbody rb;
    float pushTimer;
    private GameObject triggerPlayer;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Debug.Log("HIT");
            pushTimer = 0;
           // FindObjectOfType<AudioManager>().Play("lightning");
            triggerPlayer = other.gameObject;
            centerPos = transform.position;
            isActivated = true;
        }
    }

    private void FixedUpdate()
    {
        if (isActivated) {
            pushTimer++;
            Collider[] colliders = Physics.OverlapSphere(centerPos, radius);
            foreach (Collider hit in colliders)
            {
                if (hit != triggerPlayer.GetComponent<CapsuleCollider>())
                {
                    rb = hit.GetComponent<Rigidbody>();
                   

                    if (rb != null && pushTimer <= 100 && rb != triggerPlayer.GetComponent<Rigidbody>())
                    {
                       
                        rb.AddExplosionForce(power, centerPos, radius, 0.1f, ForceMode.Force);
                        this.gameObject.GetComponent<SphereCollider>().enabled = false;
                        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
            if (pushTimer >= 100) {
                isActivated = false;
                PowerUpSpawn.activePowerUpCount--;
                Destroy(gameObject);
            }
            Debug.Log("Timer: " + pushTimer);          
        }
    }
}
