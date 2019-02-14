using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class CheckPointProgress : MonoBehaviour
{
    public TobDisplay display;
    public int currentGate;
    public RectTransform icon;
    [Range(0, 1)]
    public float slider = 0;
    
    // Use this for initialization
    void Start() {

        currentGate = 1;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gate") {
            Debug.Log("Entering Gate");
            currentGate++;
        }
    } 

    // Update is called once per frame
    void Update ()
    {
          if (currentGate != 0)
        {
            float distToNextGate = Vector3.Distance(transform.position, display.points[currentGate-1].gate.position);


            float totaldist = Vector3.Distance(display.points[currentGate].gate.position, display.points[currentGate-1].gate.position);

            float perc =  distToNextGate / totaldist;
            icon.position = Vector3.Lerp(display.points[currentGate - 1].icon.position, display.points[currentGate].icon.position,perc);
        }
       

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(display.points[currentGate].gate.position, 1);
        if (currentGate != 0)
        {
            

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(display.points[currentGate-1].gate.position, 1);
        }
    }
}
