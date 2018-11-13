using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float thrust;
    public float turningSpeed;
    public float maxTurnAngle;
    public Rigidbody rb;

    float eulerAngX;
    float eulerAngY;
    float eulerAngZ;

    // Use this for initialization
    void Start () {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        rb.AddRelativeForce(0, 0, thrust, ForceMode.Impulse);


        //eulerAngX = transform.localEulerAngles.x;
        eulerAngY = transform.localEulerAngles.y;
        //eulerAngZ = transform.localEulerAngles.z;


        //float h = Input.GetAxis("Horizontal") * thrust * Time.deltaTime;
        //rb.AddTorque(transform.right * h);

        Vector3 turning = new Vector3();

        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (eulerAngY > 360- maxTurnAngle || eulerAngY < maxTurnAngle) 
            {
                turning = Vector3.down * turningSpeed; 
                rb.AddTorque(turning);
            }
            //print("Left arrow key is held down");
            //print(eulerAngY);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (eulerAngY > 360- maxTurnAngle || eulerAngY < maxTurnAngle)
            {
                turning = Vector3.up * turningSpeed; 
                rb.AddTorque(turning); 
            }
            //print("Right arrow key is held down");
           // print(eulerAngY); 
        }

        if (eulerAngY < 360 - maxTurnAngle && eulerAngY > 180)
        {
            eulerAngY = 360 - maxTurnAngle + 1;
            transform.localRotation = Quaternion.Euler(0, eulerAngY, 0); 
        }
        if (eulerAngY > maxTurnAngle && eulerAngY < 180) 
        {
            eulerAngY = maxTurnAngle-1; 
             transform.localRotation = Quaternion.Euler(0, eulerAngY, 0);
        }


    }

}


/*
 * 
 * 
 * 
 * 
 *         float h = Input.GetAxis("Horizontal") * amount * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * amount * Time.deltaTime;
        
        rigidbody.AddTorque(transform.up * h);
        rigidbody.AddTorque(transform.right * v);

 * */
