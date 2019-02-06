using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    //public float snowballDistance = 2f;
    //public float ballThrowForce = 5f;
    public GameObject projectile;

    public Vector3 cameraRelative;
    private Transform cam;
    private Vector3 relative;

    public float autoAimAngle = 5;
    GameObject[] gameObjects;

    // Use this for initialization
    void Start () {
        //cam = Camera.main.transform; 

        gameObjects = FindObjectsOfType<GameObject>();
        Debug.Log(gameObjects);


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

            /*
            foreach(GameObject eachGameObject in GameObject.      //  .FindGameObjectsWithTag("player"))
            {
                if(allObjects.name.Contains(""))
            }*/

            //get reference to all other objects in the scene - place into array
            var cube = GameObject.Find("TestCube");
            Debug.Log(cube); 
             var pos = cube.transform.position;
            Debug.Log(pos);

            //get angle between objects
            float angle = Vector3.Angle(transform.forward, cube.transform.forward); 
             //Debug.Log("angle: " + angle);

            //Vector3 relativePoint = transform.InverseTransformPoint(cube.transform.position);
            //Debug.Log("relative point: "+relativePoint);

            //dir'n vector player to target.
            Vector3 directionToTarget = cube.transform.position - this.transform.position;


            GameObject bullet;
            bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            
            //if (relativePoint.x > -1 && relativePoint.x < 1 && relativePoint.y > -1 && relativePoint.y < 1)
            if (angle < autoAimAngle)
            {
                bullet.GetComponent<Rigidbody>().AddForce(directionToTarget * 100); 
            }
            else
            {
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500); 
            }
            Destroy(bullet, 3); 
        }

    }
}
