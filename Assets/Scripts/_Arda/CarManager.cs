using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {

    public List<GameObject> cars = new List<GameObject>();
    public List<GameObject> headlights = new List<GameObject>();
    public float timer;
    public float currentDelay;
    public GameObject currentCar;

    public float minDelay;
    public float maxDelay;
    public float speed;


    // Use this for initialization
    void Start () {
        RandomDelay();
        currentCar = cars[0];

    
    
	}
	
	// Update is called once per frame
	void Update () {
        

        if(currentDelay > 0)
        {
            currentDelay -= Time.deltaTime;
        }

        if(currentDelay <= 0)
        {
            if(currentCar == cars[0])
            {
                currentCar.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
                headlights[0].SetActive(true);
                headlights[1].SetActive(true);
            }
            if(currentCar == cars[1])
            {
                currentCar.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1 * speed);
                headlights[2].SetActive(true);
                headlights[3].SetActive(true);
            }
            if(currentCar == cars[2])
            {
                currentCar.GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
                headlights[4].SetActive(true);
                headlights[5].SetActive(true);
            }
            if(currentCar == cars[3])
            {
                currentCar.GetComponent<Rigidbody>().velocity = new Vector3(speed * -1, 0, 0);
                headlights[6].SetActive(true);
                headlights[7].SetActive(true);
            }
        }

	}

    public void RandomDelay()
    {
        currentDelay = Random.Range(minDelay, maxDelay);
    }

    public void CurrentCar()
    {

        if (currentCar == cars[0])
        {
            currentCar = cars[1];
            headlights[0].SetActive(false);
            headlights[1].SetActive(false);
            return;
        }
        if (currentCar == cars[1])
        {
            currentCar = cars[2];
            headlights[2].SetActive(false);
            headlights[3].SetActive(false);
            return;
        }
        if (currentCar == cars[2])
        {
            currentCar = cars[3];
            headlights[4].SetActive(false);
            headlights[5].SetActive(false);
            return;
        }
        if (currentCar == cars[3])
        {
            currentCar = cars[0];
            headlights[6].SetActive(false);
            headlights[7].SetActive(false);
            return;
        }

    }

    
}
