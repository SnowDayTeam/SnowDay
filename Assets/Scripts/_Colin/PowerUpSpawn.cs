using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] powerUps;

    public static int activePowerUpCount = 0;
    [Range(1, 99)] public int maxActivePowerUps = 2;
    [Range(1, 99)] public float minSpawnTimer = 1;
    [Range(1, 99)] public float maxSpawnTimer = 10;
    public float spawnTimer;

    // Use this for initialization
    void Start ()
    {
        spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
	}
	
	// Update is called once per frame
	void Update ()
    {
        int randomPowerUp = Random.Range(0, powerUps.Length);
       
        if (activePowerUpCount < maxActivePowerUps)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0) {
                SpawnPowerUp(randomPowerUp);
                spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);

            }
        }
	}

    //spawn powerup at random location of given spawn points, if spawn point is filled, increase spawn locaiton by 1 until next available spawn
    // if no spawn is available when the default case is reached, the switch is reset to 0 to cycle back thru the list and look for an available spawn
    //will have to be polished/expanded upon to have allow for proper # of spawns to match # of cases
    //should probably change this to a looping structure, for now provides ability to have no powerup spawn if this is wanted
    void SpawnPowerUp(int powerUpToSpawn) {
        int spawnLocation = Random.Range(0, spawnPoints.Length);
        print("random spawn #: " + spawnLocation);

        while (spawnLocation < spawnPoints.Length)
        {
            if (spawnPoints[spawnLocation].childCount <= 0)
            {
                Instantiate(powerUps[powerUpToSpawn], spawnPoints[spawnLocation].transform.position, Quaternion.identity, spawnPoints[spawnLocation].transform);
                activePowerUpCount++;
                return;
            }
            else
            {
                spawnLocation++;

            }
        }
      
        //switch (spawnLocation) {
        //    case 0:
        //        if (spawnPoints[spawnLocation].childCount <= 0)
        //        {
        //            Instantiate(powerUps[powerUpToSpawn], spawnPoints[spawnLocation].transform.position, Quaternion.identity, spawnPoints[spawnLocation].transform);
        //            activePowerUpCount++;
        //        }
        //        else
        //        {
        //            spawnLocation++;

        //        }
        //        break;
        //    case 1:
        //        if (spawnPoints[spawnLocation].childCount <= 0)
        //        {
        //            Instantiate(powerUps[powerUpToSpawn], spawnPoints[spawnLocation].transform.position, Quaternion.identity, spawnPoints[spawnLocation].transform);
        //            activePowerUpCount++;
        //        }
        //        else
        //            spawnLocation++;
        //        break;
        //    case 2:
        //        if (spawnPoints[spawnLocation].childCount <= 0)
        //        {
        //            Instantiate(powerUps[powerUpToSpawn], spawnPoints[spawnLocation].transform.position, Quaternion.identity, spawnPoints[spawnLocation].transform);
        //            activePowerUpCount++;
        //        }
        //        else
        //            spawnLocation++;
        //        break;
        //    case 3:
        //        if (spawnPoints[spawnLocation].childCount <= 0)
        //        {
        //            Instantiate(powerUps[powerUpToSpawn], spawnPoints[spawnLocation].transform.position, Quaternion.identity, spawnPoints[spawnLocation].transform);
        //            activePowerUpCount++;
        //        }
        //        else
        //            spawnLocation++;
        //        break;
        //    default:
        //        spawnLocation = 0;
        //        break;

        //}
    }
}
