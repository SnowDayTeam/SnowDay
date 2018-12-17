using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour {
    private Rigidbody rb;
    public float carSpeed;
    //makes the car drive towards the camera
    public bool driveTowards;

    //handles when the car drives.
    public float currentDelay;
    public float minDelay;
    public float maxDelay;

    private float prevTime;
    private float currentTime;

    private bool newDelay=true;
    [HideInInspector]
    public bool driveMode=false;

    public GameObject LeftLight;
    public GameObject RightLight;


    //testing variables
    public float LaunchY;
    public float LaunchX;







    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	
	void Update () {
        //handles the various states of the care
        if (driveMode == true)
        {
            drive();
        }
       
        //if drive mode isn't active set up the delay then track it
        else
        {
            if (newDelay == true)
            {
                SetUpSpawnDelay();
            }

            else
            {
                CheckForDriveTime();
            }
        }
  



     
    }










    //whatever object in the trigger gets launched to the left or right so they don't get stuck underneath
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (driveTowards == false)
        {
            if(rb!= null)
            {
                rb.AddForce(new Vector3(LaunchX, LaunchY, 0));

            }
        }

        else
        {
            if (rb != null)
            {
                rb.AddForce(new Vector3(LaunchX * -1, LaunchY, 0));

            }
        }
      
    }


    //makes the car drive either toward the camera or away
    public void drive()
    {
        LeftLight.gameObject.SetActive(true);
        RightLight.gameObject.SetActive(true);
        
        if (driveTowards == false)
        {
            rb.AddForce(new Vector3(0, 0, carSpeed));
        }

        else
        {
            rb.AddForce(new Vector3(0, 0, carSpeed) * -1);
        }
    }



    //gets a random amount of time till the next car
    public void SetUpSpawnDelay()
    {
        LeftLight.gameObject.SetActive(false);
        RightLight.gameObject.SetActive(false);
        currentDelay = Random.Range(minDelay, maxDelay);
        prevTime = Time.time;
        newDelay = false;


    }
    
    //checks to see if delay time is up
    public void CheckForDriveTime()
    {
        
        if (Time.time - prevTime >= currentDelay)
        {
            driveMode = true;
            newDelay = true;

        }
    }


}
