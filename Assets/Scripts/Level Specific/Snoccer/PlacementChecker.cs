using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementChecker : MonoBehaviour
{

    private int frame = 0;
    private Transform targetPos;
    int layerMask = 1 << 8; //Layer 8

    public int framesBetweenSpherechecks = 6;
    public Transform pelvis;

    public GameObject visualizer;
    public GameObject reachPoint;

    float nearestDistance = 1000f;
    float distance = 0f;

    float visSphereRadius;

    void Start (){
        visSphereRadius = visualizer.gameObject.transform.localScale.y / 2;
    }

	void Update(){

        if (visualizer.activeSelf)
        {
            visualizer.transform.position = pelvis.position;
        }

	//Check placement only once every few frames
		frame ++;
		if (frame == framesBetweenSpherechecks){
			//print ("test");
            CheckPlacement(transform.position, visSphereRadius, layerMask);
		}
		if (frame > framesBetweenSpherechecks){
			frame = 0;
		

		}
	}

	void CheckPlacement(Vector3 center, float radius, int layer)
    {
    	//print("Test");
    	distance = 0;
    	nearestDistance = 1000f;
        Collider[] PPinRange = Physics.OverlapSphere(center, radius, layer);
 		//print(PPinRange.Length);

        int i = 0;
        while (i < PPinRange.Length)
        {
            distance = (pelvis.position - PPinRange[i].gameObject.transform.position).sqrMagnitude;
             		//print("distance " + distance + "nearestDistance " + nearestDistance);
           
            if (distance < nearestDistance) {
            		//print(PPinRange[i].gameObject.name);
               		nearestDistance = distance;
              		reachPoint.transform.position = PPinRange[i].gameObject.transform.position;
              		//print(PPinRange[i].gameObject.name);
           	}
        		i++;
        		//reachPoint.transform.position = targetPos.position;
        }
        
    }
}
