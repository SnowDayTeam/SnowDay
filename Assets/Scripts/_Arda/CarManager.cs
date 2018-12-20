using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {

    public List<GameObject> cars = new List<GameObject>();
    public int i;
    public float timer;
    public float currentDelay = 4;

    public float minDelay;
    public float maxDelay;

    // Use this for initialization
    void Start () {

    
    
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= currentDelay)
        {
            timer = 0;
        }
        
          
        


	}

    
}
