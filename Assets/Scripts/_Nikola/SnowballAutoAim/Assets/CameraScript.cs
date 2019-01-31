using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float snowballDistance = 2f;
    public float ballThrowForce = 5f;
    public GameObject projectile;

    public Vector3 cameraRelative;
    private Transform cam;
    private Vector3 relative;

    

    // Use this for initialization
    void Start () {
        cam = Camera.main.transform; 
         Vector3 cameraRelative = cam.InverseTransformPoint(transform.position); 

    }
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    //called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetMouseButtonDown(0))
        {
            //if (cameraRelative.z > 0)
            //{
                GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500); 
                 Destroy(bullet, 3);
           // }

            
        }

    }
}
