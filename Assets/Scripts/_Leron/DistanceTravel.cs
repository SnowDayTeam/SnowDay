using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTravel : MonoBehaviour {

    public Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

  
        
    }
    /*
    public float distanceTravelled = 0;
    Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        print(distanceTravelled/100);
    }

    */

