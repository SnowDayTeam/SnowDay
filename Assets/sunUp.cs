using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunUp : MonoBehaviour {
    ParticlesAreaManipulator PAM;
    public float speed;
	// Use this for initialization
	void Start () {
        PAM = GetComponent<ParticlesAreaManipulator>();
	}
	
	// Update is called once per frame
	void Update () {
        PAM.m_strength += Time.deltaTime * speed;
	}
}
