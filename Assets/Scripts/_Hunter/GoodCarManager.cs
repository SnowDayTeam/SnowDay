using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodCarManager : MonoBehaviour {

    
    [System.Serializable]
    struct CarPathingArrays
    {
        public GameObject[] waypoints;          
        public GameObject[] reverseWaypoints;   
        public GameObject[] waypointsToExit;
    }

    [SerializeField]
     CarPathingArrays[] CarInfo;

    public float[] DelayRange;
    float TimeWaitStarted;
    public float CurrentDelay;
    public bool CarSpawned = false;
    public bool DelayStarted;
    public Transform PrefabToSpawn;


    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //If delay hasn't been started set it up
        if (!DelayStarted)
        {
            GetDelayTime();
        }
        
        //If set up complete initate actual spawning sequence
        else
        {
            if (Time.time - TimeWaitStarted > CurrentDelay&& !CarSpawned)
            {
                CarSpawned = true;
                int Path = Random.Range(0,CarInfo.Length);


                //Instantiate the car prefab and set up all it's variables
                Transform ItemInstance= Instantiate(PrefabToSpawn, CarInfo[Path].waypoints[0].transform);
                //ItemInstance.position = CarInfo.waypoints[0].transform.position;
                ItemInstance.position = transform.position;

                //Set up all the cars variables

                ItemInstance.GetComponent<CarPathingFinding>().waypoints = CarInfo[Path].waypoints;
                ItemInstance.GetComponent<CarPathingFinding>().reverseWaypoints = CarInfo[Path].reverseWaypoints;
                ItemInstance.GetComponent<CarPathingFinding>().waypointsToExit = CarInfo[Path].waypointsToExit;
                ItemInstance.GetComponent<CarPathingFinding>().CarSpawner = this;

            }
        }
        
		
	}

    public void GetDelayTime()
    {
        //Sets Up Next Spawn
        CurrentDelay = Random.Range(DelayRange[0], DelayRange[1]);
        TimeWaitStarted = Time.time;
        DelayStarted = true;

    }
}
