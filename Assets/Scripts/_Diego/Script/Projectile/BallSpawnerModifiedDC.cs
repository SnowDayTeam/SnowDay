using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class BallSpawnerModifiedDC : MonoBehaviour
{
    //private Vector3 offset = new Vector3(1, 0, 1);
    //public GameObject prefab;
    //public GameObject spawnPoint;
    public ProjectileLauncher projectileLauncher;
    public PlayerActor mySelf;
   // public float LaunchVelocity;
    //public float throwDelay;
    private float timeFired;
    public PierInputManager.ButtonName ShootButton = PierInputManager.ButtonName.X;
    private PierInputManager playerInputController;

    //SnowPick Up
 //   private bool BallPickedUp;
	//public FullBodyBipedIK IK;
	//private bool IsReaching;
	//public float lerpTime = 1f;
 //   float currentLerpTime;
	//public Transform ReachPoint;
 //   private bool DoneReaching;


    
    

	// Use this for initialization
	void Start () {
        playerInputController = gameObject.GetComponentInParent<PierInputManager>();
       // IK = gameObject.GetComponentInParent<FullBodyBipedIK>();
    }

    // Update is called once per frame
    void Update () {
        if (playerInputController.GetButtonDown(ShootButton))
        {
            ThrowBall();

            //if (!BallPickedUp)
            //{
            //   // print("Hit A Picking Up");
            //    PickUpBall();
            //}
            ////spawns in ball at spawn point and launches ball in faced direction
            //else
            //{
            //    print("Hit A Throwing");
               
            //}

            //timeFired = Time.time;
          
        }

  //      if (IsReaching==true)
  //      {
		//	print("LerpingIN");
		//	currentLerpTime += Time.deltaTime;
		//	if (currentLerpTime > lerpTime)
  //          {
  //              currentLerpTime = lerpTime;
  //          }

  //          float t = currentLerpTime / lerpTime;
  //          IK.solver.rightHandEffector.target = ReachPoint;
  //          IK.solver.rightHandEffector.positionWeight = Mathf.Lerp(IK.solver.rightHandEffector.positionWeight, 1.0f, t);

		//	if(IK.solver.rightHandEffector.positionWeight==1)

		//	{
		//		DoneReaching = true;
		//		IsReaching = false;

		//		print("Reached Ground");

		//	}

		//}


		//if(DoneReaching==true)
		//{
		//	print("Lerp Out");
		//	currentLerpTime = 0f;
		//	currentLerpTime += Time.deltaTime;
  //          if (currentLerpTime > lerpTime)
  //          {
  //              currentLerpTime = lerpTime;
  //          }

  //          float t = currentLerpTime / lerpTime;
            
  //          IK.solver.rightHandEffector.positionWeight = Mathf.Lerp(IK.solver.rightHandEffector.positionWeight, 0f, t);




  //          if (IK.solver.rightHandEffector.positionWeight <= 0.1)
		//	{
		//		print("PICKED UP");
		//		IK.solver.rightHandEffector.target = null;
		//		BallPickedUp = true;
		//		DoneReaching = false;

				
		//	}
		//}	   
    }



    //private void PickUpBall()
    //{
    //	IsReaching = true;
    //}

    private void ThrowBall()
    {
        projectileLauncher.LaunchProjectile(mySelf);
       // BallPickedUp = false;
    }



}
