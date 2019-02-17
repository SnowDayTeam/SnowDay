using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBackCar : MonoBehaviour {

    public bool MoveingTowards;
    public GameObject awayStart;
    public GameObject towardStart;
    public GameObject carManager;

    private void OnTriggerEnter(Collider other)
    {
        //makes the car immobile
        if (other.tag == "Car")
        {
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            carManager.GetComponent<CarManager>().CurrentCar();
            carManager.GetComponent<CarManager>().RandomDelay();

            /*if(other.GetComponent<CarDriving>() != null)
               other.GetComponent<CarDriving>().driveMode = false;
            if (other.GetComponent<Car>() != null)
                other.GetComponent<Car>().driveMode = false;
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);*/


            //sets the car back on its starting position based on the lane
            if (MoveingTowards == false)
            {
                other.transform.position = awayStart.transform.position;
            }

            else
            {
                other.transform.position = towardStart.transform.position;
            }
        }
    }
  }

