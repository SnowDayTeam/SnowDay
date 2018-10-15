using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{

    public GameObject RespawnPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // if player is overlapping transform them to a certain point
        if (other.tag == "Player")
        {
            other.transform.position = RespawnPoint.transform.position;
            print("respawn Player");
        }
    }

}
