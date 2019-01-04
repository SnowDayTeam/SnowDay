using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityStandardAssets;
using RootMotion.FinalIK;

public class CharacterSpecificVariables : MonoBehaviour {

    public bool soccerSetup;
    public bool soccerStrip;
    [Tooltip("tip")]

    [Header("IK")]
    public float IKWeight = 0.7f;
    public int IKIterations = 1;
    FullBodyBipedIK IK;
    public GameObject reachDetectBox;
    GameObject reachBoxAtStart;
    [Header("Throwing Snowball")]
    public float throwingPower = 20;
    public float throwingDelay = 0.5f;
    public GameObject snowBall;
    Transform throwHand;
    Transform controller;
    GameObject releasePoint;
    BallSpawner ballSpawner;
    GameObject throwPoint;





    
    private void Update()
    {
        if (soccerSetup) SoccerSetup();
        if (soccerStrip) SoccerStrip();
    }


    public void SoccerSetup () {

        //---------------------------------------------------------------------------------------
        //Setup
        //---------------------------------------------------------------------------------------
        //ref to Reachbox Prefab
        reachBoxAtStart = reachDetectBox;
        //get ref to bones
        throwHand = GetComponentInChildren<Wrist_R>().transform;
        controller = GetComponentInChildren<Animator>().transform;
        print(controller.name);

        //Set up IK
        IK = controller.GetComponent<FullBodyBipedIK>();
        IK.solver.IKPositionWeight = IKWeight;
        IK.solver.iterations = IKIterations;

    //---------------------------------------------------------------------------------------
    //create throwing  point (Where ball leaves the hand)
    //---------------------------------------------------------------------------------------
        throwPoint = new GameObject();
        throwPoint.name = "Throw Point";
        Instantiate(throwPoint, throwHand.position, throwHand.rotation);
        throwPoint.transform.parent = throwHand;
        throwPoint.transform.localPosition = new Vector3(0.07058f, -0.11f, 0);
   

    //---------------------------------------------------------------------------------------
    //create release  point (above the shoulder)
    //---------------------------------------------------------------------------------------
        releasePoint = new GameObject();
        releasePoint.name = "Snowball release Point";
        Instantiate(releasePoint, Vector3.zero, Quaternion.identity);
        releasePoint.transform.parent = throwHand.parent.parent;
        releasePoint.transform.localPosition = new Vector3(-0.2f, 0.35f, 0);
        //Get Diego's Ball Spawner
        ballSpawner = releasePoint.AddComponent<BallSpawner>();
        ballSpawner.inputName = "FireP1";
        ballSpawner.IK = IK;
        ballSpawner.ReachPoint = throwPoint.transform;
        ballSpawner.prefab = snowBall;
        ballSpawner.LaunchVelocity = throwingPower;
        // ballSpawner.projectileLauncher = ProjectileLauncher;




        //---------------------------------------------------------------------------------------
        //Set Up Trigger Boxes
        //---------------------------------------------------------------------------------------


        reachDetectBox =  Instantiate(reachDetectBox, new Vector3(controller.position.x, controller.position.y, controller.position.z), controller.rotation);
        reachDetectBox.transform.parent = controller;

        reachDetectBox.transform.GetChild(0).GetComponent<ReachR>().IK = controller.GetComponent<FullBodyBipedIK>();
        reachDetectBox.transform.GetChild(1).GetComponent<ReachL>().IK = controller.GetComponent<FullBodyBipedIK>();
        if (GameObject.FindWithTag("SnowBall") != null)
        {
            reachDetectBox.transform.GetChild(0).GetComponent<ReachR>().ball = GameObject.FindWithTag("SnowBall").transform;
            reachDetectBox.transform.GetChild(1).GetComponent<ReachL>().ball = GameObject.FindWithTag("SnowBall").transform;
        }

    //---------------------------------------------------------------------------------------
    //Set Up Character Controller
    //---------------------------------------------------------------------------------------
    

        soccerSetup = false;
        print("test");
	}
    public void SoccerStrip() {
        print("strip soccer");
        IK.solver.leftHandEffector.positionWeight = 0;
        IK.solver.rightHandEffector.positionWeight = 0;
        IK = null;
        controller = null;
        Destroy(throwPoint);
        Destroy(releasePoint);
        ballSpawner = null;
        Destroy(reachDetectBox.gameObject);
        reachDetectBox = reachBoxAtStart;

        soccerStrip = false;

    }
}
//[CustomEditor(typeof(CharacterSpecificVariables))]
//public class ObjectBuilderEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        CharacterSpecificVariables myScript = (CharacterSpecificVariables)target;
//        if (GUILayout.Button("setup"))
//        {
//            myScript.SoccerSetup();
//        }
//    }
//}