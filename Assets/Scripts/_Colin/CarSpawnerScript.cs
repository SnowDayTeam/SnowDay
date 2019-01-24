using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerScript : MonoBehaviour {

    public Transform SpawnPoint;
    public List<GameObject> carPrefabs = new List<GameObject>();

    public static int activeCarCount = 0;
    [Range(1, 99)] public int maxActiveCars;
    [Range(1, 99)] public float minSpawnTimer;
    [Range(1, 99)] public float maxSpawnTimer;
    public float spawnTimer;


    // Use this for initialization
    void Start () {
        spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
	}
	
	// Update is called once per frame
	void Update () {
        if (activeCarCount < maxActiveCars) {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0) {
                SpawnCar();
                activeCarCount++;
                spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
            }
        }
	}

    void SpawnCar() {
        int carToSpawn = Random.Range(0, 2);
        if (carToSpawn == 0) {
            Instantiate(carPrefabs[0], SpawnPoint.position, SpawnPoint.rotation);
        }
        if (carToSpawn == 1)
        {
            Instantiate(carPrefabs[1], SpawnPoint.position, SpawnPoint.rotation);
        }
    }
}
