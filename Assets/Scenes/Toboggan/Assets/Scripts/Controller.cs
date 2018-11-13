using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float thrust;
    public float turningSpeed;
    public float maxTurnAngle;
    public Rigidbody rb;

    private float targetSteerAngle = 0;
    private float steerAngle = 0;

    float eulerAngX;
    float eulerAngY;
    float eulerAngZ;

    // Use this for initialization
    void Start () {
        //rb = GetComponent<Rigidbody>();
    }

    void Update () {
        rb.AddRelativeForce(0, 0, thrust, ForceMode.Impulse); 

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //if vehicle is able to turn farther left
            if (targetSteerAngle > 0-maxTurnAngle /* && targetSteerAngle < maxTurnAngle*/) 
            {
                //turn target angle left
                targetSteerAngle--;
                //make actual angle of vehicle progress towards targeted angle
                steerAngle = Mathf.Lerp(steerAngle, targetSteerAngle, Time.deltaTime * turningSpeed);
                transform.localRotation = Quaternion.Euler(0, steerAngle, 0); 
            }
            else
            {
                //increase target steering angle to be within range
                targetSteerAngle++;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //if vehicle is able to turn farther left
            if (targetSteerAngle < maxTurnAngle)
            {
                //turn target angle right
                targetSteerAngle++;
                //make actual angle of vehicle progress towards targeted angle
                steerAngle = Mathf.Lerp(steerAngle, targetSteerAngle, Time.deltaTime * turningSpeed);
                transform.localRotation = Quaternion.Euler(0, steerAngle, 0); 
            }
            else
            {
                //reduce target steering angle to be within range
                targetSteerAngle--;
            }
        }
    }
}
