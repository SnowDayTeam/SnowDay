using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCarBack : MonoBehaviour {
    public bool MoveingTowards;
    private Vector3 awayStart = new Vector3(21.24f, 0.87f, -21.91f);
    private Vector3 towardStart = new Vector3(16.4f, 0.87f, 23.42f);
 

    private void OnTriggerEnter(Collider other)
    {
        //makes the car immobile
        if (other.tag == "Car")
        {
            other.GetComponent<CarDriving>().driveMode=false;
            other.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);


            //sets the car back on its starting position based on the lane
            if (MoveingTowards == false)
            {
                other.transform.position = awayStart;
            }

            else
            {
                other.transform.position = towardStart;
            }
        }
    }



}
