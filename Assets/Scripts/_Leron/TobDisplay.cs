using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]


public class TobDisplay : MonoBehaviour
{
    [System.Serializable]


   public struct checkPointsCheck{
        public float distanceToNextCheckPoint;
        public Transform gate;
        public RectTransform icon;
        public float screenPercentage; 

    }

    public checkPointsCheck[] points;
  
    [Space]
    public float totalDistance;
	// Use this for initilization
	void Start ()
    {
        updateDistance();

        splitCheckPoint();

    }

    public void splitCheckPoint()
    {
        for (int i = 0; i < points.Length; i++)
        {
            float dist = 0;
            if (i !=0)
            {
                dist = Vector3.Distance(points[i].gate.position, points[i - 1].gate.position);
            }
            else
            {
               // dist = Vector3.Distance(points[i].gate.position, points[0].gate.position);

            }

            float screenPercent = dist / totalDistance;
            points[i].screenPercentage = screenPercent;
          //  Debug.Log(i.ToString() + "  " + screenPercent.ToString());
            if (i == points.Length - 1) //end of race
            {
                points[i].icon.position = new Vector2((Screen.width), points[i].icon.parent.position.y);
               // Debug.Log("test");
            }
            else if (i == 0)  //start of race
            {
                points[i].icon.position = new Vector2((0), points[i].icon.parent.position.y);

            }
            else if (i != 0)
            {
                points[i].icon.position = new Vector2((Screen.width * points[i].screenPercentage + points[i - 1].icon.position.x), points[i].icon.parent.position.y);

            }
        
             

        }
    }
    [ContextMenu("updateDistance")]
    public void updateDistance()
    {
        totalDistance = GetTotalDistance();

    }
    float GetTotalDistance()
    {
        float dist = 0;
        for (int i = 0; i < points.Length; i++)
        {
            if (i != 0)
            {
                //   Gizmos.DrawLine(checkPoints[i].position, checkPoints[i - 1].position);
                dist += Vector3.Distance(points[i].gate.position, points[i - 1].gate.position);
            }
            else
            {
               // dist += Vector3.Distance(points[i].gate.position, points[points.Length - 1].gate.position);

            }
        }

       
        return dist;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < points.Length; i++)
        {
            if(i != 0)
            {
                Gizmos.DrawLine(points[i].gate.position, points[i - 1].gate.position);

            }
            else
            {
              //  Gizmos.DrawLine(points[i].gate.position, points[points.Length-1].gate.position);
                
            }
        }

    }
    // Update is called once per frame
    void Update ()
    {
        updateDistance();

        splitCheckPoint();
    }
}
