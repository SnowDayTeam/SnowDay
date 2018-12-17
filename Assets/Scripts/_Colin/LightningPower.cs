using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightningPower : MonoBehaviour {

    public bool isActivated;
    public Vector3 centerPos;
    [Range(10, 100)]
    public float radius;
    [Range(5, 200)]
    public float power;
    Rigidbody rb;
    float pushTimer;
    private GameObject triggerPlayer;

    private void Start()
    {
        pushTimer = 0;
        radius = 10.0f;
        power = 75.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            pushTimer = 0;
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
                rb = hit.GetComponent<Rigidbody>();

                if (rb != null && pushTimer <= 100 && rb!= triggerPlayer.GetComponent<Rigidbody>())
                {
                    rb.AddExplosionForce(power, centerPos, radius, 0.1f,ForceMode.Force);
                }
            }
            if (pushTimer >= 100) {
                isActivated = false;
            }
            Debug.Log("Timer: " + pushTimer);          
        }
    }
}
