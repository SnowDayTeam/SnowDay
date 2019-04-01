using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGateTrigger : MonoBehaviour
{

    bool up;
    public float speed;
    public GameObject gate;
    Animator animator;

    void Start()
    {
        animator = gate.GetComponent<Animator>();
    }

    void Update()
    {
        if (up == true)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            animator.SetTrigger("Close");
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            up = true;
        }

    }



    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            up = false;
        }

    }
}