using UnityEngine;
using System.Collections;

public class FlagSpawner : MonoBehaviour 
{
    
    static public FlagSpawner Instance = null;

    [Header("Flag Variables")]
    public int MaxNumberFlags = 1;
    public int CurrentNumberFlags = 1;

    [Header("Spawn Variables")]
    [SerializeField] GameObject FlagPrefab = null;
    [SerializeField] Transform[] SpawnPoints = null;
    [Tooltip("The extra distance added randomly to the x and z positions between 0 and this value")]
    [SerializeField] float ExtraSpawnDistance = 1.0f;
    [SerializeField] float FlagSpawnDelay = 1.0f;

    [Header("Spawn Location Gizmos")]
    [SerializeField] Color GizmoColor = Color.green;
    [SerializeField] float GizmoSize = 1.0f;

    bool CanSpawnFlag = true;
    const float FlagYHeight = 1.0f;

    void Awake() 
    {
        FlagSpawner.Instance = this;
    }

    void Update()
    {
        if(this.CurrentNumberFlags < this.MaxNumberFlags && this.CanSpawnFlag)
        {
            this.StartCoroutine(this.SpawnFlag());
        }
    }

    IEnumerator SpawnFlag() 
    {
        this.CanSpawnFlag = false;
        yield return new WaitForSeconds(this.FlagSpawnDelay);

        Vector3 RandomPosition = new Vector3(Random.Range(0.0f, this.ExtraSpawnDistance),
                                        FlagYHeight, Random.Range(0.0f, this.ExtraSpawnDistance));

        Instantiate(this.FlagPrefab, 
            this.SpawnPoints[Random.Range(0, this.SpawnPoints.Length)].position 
                                            + RandomPosition, Quaternion.identity);

        this.CurrentNumberFlags++;
        this.CanSpawnFlag = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = this.GizmoColor;
        foreach(Transform SpawnPoint in this.SpawnPoints) 
        {
            if(!SpawnPoint)
                return;

            Gizmos.DrawSphere(SpawnPoint.transform.position, this.GizmoSize);
        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class FlagSpawner : MonoBehaviour {

//    //Actor references
//    //[SerializeField]
//    //CTFGameManager GM;
//    [SerializeField]
//    CaptureTheFlagGamemodeManager FlagGamemodeManager = null;
    
//    public bool alreadySpawned=false;
//    [SerializeField]
//    GameObject Flag;

//    //Spawn position range 
//    public float xSpawnRange=3;
//    public float zSpawnRange =3;

//    //variables for setting spawn timer
//    public float spawnTimeMax=20;
//    public float SpawnTimeMin=5;
//    public float currentspawnTimer;
//    float Timestarted;
//    bool TimeSet=false;

//    public Color gizmoColor = Color.green;
//    public void OnDrawGizmos()
//    {
//        Gizmos.color = gizmoColor;
//        Gizmos.DrawSphere(transform.position, .5f);
//    }

//    // Use this for initialization
//    void Start ()
//    {
//       // FlagGamemodeManager = GameObject.FindObjectOfType<CaptureTheFlagGamemodeManager>();

//    }
	
//	// Update is called once per frame
//	void Update ()
//    {
//        if (TimeSet == false && alreadySpawned==false)
//        {
//            //Debug.Log("Set Timer");
//            SetSpawnTimer();
//        }
     
//        if (alreadySpawned==false && FlagGamemodeManager.CurrentFlags < FlagGamemodeManager.MaxFlags)
//        {
//           // Debug.Log("CheckingTime");
//            if (Time.time - Timestarted > currentspawnTimer)
//            {
//                Debug.Log("SpawnFlag");
//                SpawnFlag();
//            }
//        }

//        else
//        {
//            TimeSet = false;
//        }
//	}



//    void SetSpawnTimer()
//    {
//        currentspawnTimer = Random.Range(SpawnTimeMin, spawnTimeMax);
//        Timestarted = Time.time;
//        TimeSet = true;
//    }

//    void SpawnFlag()
//    {
//        //Spawn the flag and give a reference to spawner to flag
//        GameObject flagspawned;
//        flagspawned= Instantiate(Flag, new Vector3(transform.position.x + Random.Range(xSpawnRange*-1,xSpawnRange), transform.position.y+ 1.34f, transform.position.z+ Random.Range(zSpawnRange*-1,zSpawnRange)), Quaternion.identity);
//        flagspawned.GetComponent<FlagPickup>().spawner = gameObject.GetComponent<FlagSpawner>();
//        //Set variables so spawner can be good to go later on
//        FlagGamemodeManager.CurrentFlags++;
//        alreadySpawned = true;
//        TimeSet = false;
//    }
//}
