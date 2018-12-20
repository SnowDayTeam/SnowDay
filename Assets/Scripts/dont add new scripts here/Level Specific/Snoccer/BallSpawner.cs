using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class BallSpawner : MonoBehaviour {
    private Vector3 offset = new Vector3(1, 0, 1);
    public GameObject prefab;
    public GameObject spawnPoint;
    public float LaunchVelocity;
    public float throwDelay;
    private float timeFired;
    public string inputName;
	//SnowPick Up
	public bool BallPickedUp;
	public FullBodyBipedIK IK;
	public bool IsReaching;
	public float lerpTime = 1f;
    float currentLerpTime;
	public Transform ReachPoint;
	public bool DoneReaching;


    
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(IsReaching==true){
			//print("LerpingIN");
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float t = currentLerpTime / lerpTime;
            IK.solver.rightHandEffector.target = ReachPoint;
            IK.solver.rightHandEffector.positionWeight = Mathf.Lerp(IK.solver.rightHandEffector.positionWeight, 1.0f, t);

			if(IK.solver.rightHandEffector.positionWeight==1)

			{
				DoneReaching = true;
				IsReaching = false;

				

			}

		}


		if(DoneReaching==true)
		{
		
			currentLerpTime = 0f;
			currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float t = currentLerpTime / lerpTime;
            
            IK.solver.rightHandEffector.positionWeight = Mathf.Lerp(IK.solver.rightHandEffector.positionWeight, 0f, t);




            if (IK.solver.rightHandEffector.positionWeight <= 0.1)
			{
				
				IK.solver.rightHandEffector.target = null;
				BallPickedUp = true;
				DoneReaching = false;

				
			}
		}

        
		if (Input.GetButtonDown(inputName))
        {
			
			if(!BallPickedUp)
			{
				
				PickUpBall();
			}
			//spawns in ball at spawn point and launches ball in faced direction
			else
			{
				
				ThrowBall();
			}

            timeFired = Time.time;
            print(inputName);
            
            
        }




        
    }
    


	private void PickUpBall()
	{

		IsReaching = true;



	}

	private void ThrowBall()
	{
		GameObject throwBall = Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        throwBall.GetComponent<Rigidbody>().velocity = transform.forward * LaunchVelocity;
		BallPickedUp = false;
	}



}
