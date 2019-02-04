using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlagSpawner : MonoBehaviour {

    //Actor references
    [SerializeField]
    CTFGameManager GM;
    
    public bool alreadySpawned=false;
    [SerializeField]
    GameObject Flag;

    //Spawn position range 
    public float xSpawnRange=3;
    public float zSpawnRange =3;

    //variables for setting spawn timer
    public float spawnTimeMax=20;
    public float SpawnTimeMin=5;
    public float currentspawnTimer;
    float Timestarted;
    bool TimeSet=false;



    public Color gizmoColor = Color.green;
    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, .5f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TimeSet == false && alreadySpawned==false)
        {
            //Debug.Log("Set Timer");
            SetSpawnTimer();
        }
     
        if (alreadySpawned==false && GM.currentFlags < GM.MaxFlags)
        {
           // Debug.Log("CheckingTime");
            if (Time.time - Timestarted > currentspawnTimer)
            {
                Debug.Log("SpawnFlag");
                SpawnFlag();
            }
        }

        else
        {
            TimeSet = false;
        }
	}



    void SetSpawnTimer()
    {
        currentspawnTimer = Random.Range(SpawnTimeMin, spawnTimeMax);
        Timestarted = Time.time;
        TimeSet = true;
    }

    void SpawnFlag()
    {
        //Spawn the flag and give a reference to spawner to flag
        GameObject flagspawned;
        flagspawned= Instantiate(Flag, new Vector3(transform.position.x + Random.Range(xSpawnRange*-1,xSpawnRange), transform.position.y+ 1.34f, transform.position.z+ Random.Range(zSpawnRange*-1,zSpawnRange)), Quaternion.identity);
        flagspawned.GetComponent<FlagPickup>().spawner = gameObject.GetComponent<FlagSpawner>();
        //Set variables so spawner can be good to go later on
        GM.currentFlags++;
        alreadySpawned = true;
        TimeSet = false;
    }
}
