using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProgress : MonoBehaviour {

    void Start()
    {
      transform.position = new Vector2((Screen.width/ Screen.width) + 5, transform.parent.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject thePlayer = GameObject.Find("Board");
        Rigidbody speed = thePlayer.GetComponent<Rigidbody>();

        if(transform.position.x < (Screen.width - 7))
        transform.position = new Vector2(transform.position.x + (speed.velocity.magnitude + 0.09f), transform.parent.position.y);
      
    }


}
