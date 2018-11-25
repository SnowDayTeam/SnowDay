using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    //forwards force on sled, multiplied by 50 in add force
    public float thrust = 1;
    //sideways force (steering) multiplied by 100 in keyDown
    public float steerThrust = 4;

    public Rigidbody rb;
    public Text speedText;
    public float maxSpeed = 25;

    float distFromGround = 0.75f;


    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        speedText.text = rb.velocity.z.ToString(); 


        //increase speed if under max
        if (rb.velocity.z < maxSpeed) 
            rb.AddRelativeForce(0, 0, thrust*50, ForceMode.Force);
        else if (rb.velocity.z > maxSpeed + 1) 
            rb.AddRelativeForce(0, 0, -thrust*25, ForceMode.Force);
        //decrease speed if more than 1 above max

        //only add steering input if grounded
        if (Input.GetKey(KeyCode.LeftArrow) && IsGrounded()) 
        {
            rb.AddRelativeForce(-steerThrust*100, 0, 0, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.RightArrow) && IsGrounded())
        {
            rb.AddRelativeForce(steerThrust*100, 0, 0, ForceMode.Force);
        }
        if(!IsGrounded())
            Debug.Log("Off Ground"); 
        }

    bool IsGrounded()
    {
        //cast ray to below player object, detecting if ground is within distance (distFromGround variable)
        return Physics.Raycast(transform.position, -Vector3.up, distFromGround); 
    }

}