using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTest : MonoBehaviour {
    public bool test;
    Rigidbody rb;
    [Range(10, 100)]
    public float radius;
    public float power;
    Rigidbody rb1;
    float timer = 2f;
    private GameObject triggerPlayer;

    // Use this for initialization
    void Start () {
        rb1 = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        
    }
    void LightningStrike()
    {
        rb.AddExplosionForce(power, transform.position, radius, 0.1f, ForceMode.Impulse);

    }
}
