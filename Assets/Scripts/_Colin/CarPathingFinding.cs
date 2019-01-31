using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarPathingFinding : MonoBehaviour {

    //stuff for movement
    public float slowDistPerc;
    private float slowDist;
    public float stopDist;
    public float slowRotPerc;
    private float slowRot;
    private float rotLeft;

    private float velocity = 0.0F; // Linear velocity.
    private float rotation = 0.0F; // Angular velocity.
    [Range(1, 99)] public float velocityMax; //max velocity
    [Range(1, 180)] public float rotationMax;  //max rotation angle
    private float accelLinear;
    private float accelAngular;
    public float accelLinearInc;
    public float accelAngularInc;
    [Range(1, 99)] public float accelLinearMax;
    [Range(1, 180)] public float accelAngularMax;

    private bool isParking = false;
    private bool isParked = false;
    private bool isReversing = false;
    private bool isExiting = false;


    private Vector3 moveTarget;     // Temporary variable to keep the position to go to
    private Vector3 destVect;       // toward the position to go
    private float distTo;           // The magnitude of destVect
    private Quaternion destRot;     // The rotation angle of destVect 

    private int waypointIndex = 0;
    public GameObject[] waypoints;
    private int pathToFollow;
    [SerializeField] float randomParkTime;
    private float currentMaxVelocity;

    void Start () {
        moveTarget = waypoints[waypointIndex].transform.position;
        isParking = true;
        randomParkTime = UnityEngine.Random.Range(3, 5);
        StartMoving();
        currentMaxVelocity = velocityMax;
	}
	
	// Update is called once per frame
	void Update () {
        if (isParking)
        {
            // "Set the destination vector and rotation"
            destVect = moveTarget - transform.position;

            distTo = destVect.magnitude;
            destRot = Quaternion.LookRotation(destVect);

            //*** Main Movement ***
            transform.Translate(Vector3.forward * GetMoveSpeed() * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, GetRotSpeed() * Time.deltaTime);

            velocity = Mathf.Clamp(velocity + accelLinear, 0.0F, currentMaxVelocity);
            accelLinear = Mathf.Clamp(accelLinear + accelLinearInc, 0.0F, accelLinearMax);

            rotation = Mathf.Clamp((rotation + accelAngular), 0.0F, rotationMax);
            accelAngular = Mathf.Clamp((accelAngular + accelAngularInc), 0.0F, accelAngularMax);

            if (distTo < stopDist && waypointIndex == waypoints.Length - 2)
            {
                // last waypoint, slow car into parking space
                currentMaxVelocity = currentMaxVelocity / distTo;
                print("slowdown");               
            }
            if (distTo < stopDist && waypointIndex == waypoints.Length - 1)
            {              
                // All waypoints met
                // All waypoints are met to park car, reverse path to send car to spawn and destory it
                velocity = 0.0F;
                print("Parked");
                Array.Reverse(waypoints);
                print(waypoints[0]);
                isParking = false;
                isParked = true;
                //create delay here for random amount of parked time, then reverse waypoints array and reverse car out, then drive car forward to exit

            }
            else if (distTo < stopDist && waypointIndex != waypoints.Length - 1 && waypointIndex != waypoints.Length)
            {       // There is still waypoint to meet and it goes to the next waypoint
                waypointIndex++;
                if (waypointIndex == waypoints.Length)
                    waypointIndex = 0;
                moveTarget = waypoints[waypointIndex].transform.position;
                StartMoving();
            }
        }

        if (isParked) {
            randomParkTime -= Time.deltaTime;
            if (randomParkTime <= 0)
            {
                isParked = false;
                //need to reset waypoint index after parking to allow cars to repath out the way they came in
                waypointIndex = 0;
                isReversing = true;
                
            }
        }
        
        // not working as intended, cars currently either back up forever, or this state is skipped
        if (isReversing) {
          
            destVect = moveTarget - transform.position;

            distTo = destVect.magnitude;
            destRot = Quaternion.LookRotation(destVect);

            //*** Main Movement ***
            transform.Translate(Vector3.back * GetMoveSpeed() * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, GetRotSpeed() * Time.deltaTime);

            velocity = Mathf.Clamp(velocity + accelLinear, 0.0F, currentMaxVelocity);
            accelLinear = Mathf.Clamp(accelLinear + accelLinearInc, 0.0F, accelLinearMax);

            rotation = Mathf.Clamp((rotation + accelAngular), 0.0F, rotationMax);
            accelAngular = Mathf.Clamp((accelAngular + accelAngularInc), 0.0F, accelAngularMax);

             if (distTo < stopDist && waypointIndex != waypoints.Length)
             {
                 // reverse from space slowly
                 print("reversing");
                 waypointIndex++;
                 print(waypointIndex);
                 moveTarget = waypoints[waypointIndex].transform.position;
                 //StartMoving();
             }
            if (distTo < stopDist && waypointIndex == 1) {
                isReversing = false;
                isExiting = true;
                print("stop reversing");
            }
        }
        
        if (isExiting) {

            destVect = moveTarget - transform.position;

            distTo = destVect.magnitude;
            destRot = Quaternion.LookRotation(destVect);

            //*** Main Movement ***
            transform.Translate(Vector3.forward * GetMoveSpeed() * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, GetRotSpeed() * Time.deltaTime);

            velocity = Mathf.Clamp(velocity + accelLinear, 0.0F, velocityMax);
            accelLinear = Mathf.Clamp(accelLinear + accelLinearInc, 0.0F, accelLinearMax);

            rotation = Mathf.Clamp((rotation + accelAngular), 0.0F, rotationMax);
            accelAngular = Mathf.Clamp((accelAngular + accelAngularInc), 0.0F, accelAngularMax);


            if (distTo < stopDist && waypointIndex == waypoints.Length - 1)
            {
                // All waypoints met, remove car from scene, reduce the number of active cars 
                CarSpawnerScript.activeCarCount--;
                Destroy(gameObject);

            }
            else if (distTo < stopDist && waypointIndex != waypoints.Length - 1 && waypointIndex != waypoints.Length)
            {      
                // There is still waypoint to meet and it goes to the next waypoint
                waypointIndex++;
                moveTarget = waypoints[waypointIndex].transform.position;
                StartMoving();
            }

        }
    }

    void StartMoving()
    {
        accelLinear = 0.0F;
        accelAngular = 0.0F;
        rotation = 0.0F;

        destVect = moveTarget - transform.position;
        distTo = destVect.magnitude;
        slowDist = distTo * slowDistPerc;

        destRot = Quaternion.LookRotation(destVect);
        rotLeft = Quaternion.Angle(transform.rotation, destRot);    // Returns the angle difference between the direction of WaypointSeek and angle of destVect
        slowRot = rotLeft * slowRotPerc;
    }

    float GetMoveSpeed()
    {
        return ((distTo >= slowDist) ? velocity : Mathf.Lerp(0.0F, velocity, distTo / slowDist)); //Linearly interpolates between a and b by t.The parameter t is clamped to the range[0, 1].
    }

    float GetRotSpeed()
    {
        return ((rotLeft >= slowRot) ? rotation : Mathf.Lerp(0.0F, rotation, rotLeft / slowRot));
    }
}
