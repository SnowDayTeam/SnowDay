using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FlagController : MonoBehaviour {

    [SerializeField]
    float PickUpDelay = 3;
    float CurrentHoldTime = 0;
    float HoldStarted;
    [HideInInspector]
    public bool TryingToPickUp=false;
    [HideInInspector]
    public bool IsHolding = false;
    [HideInInspector]
    public GameObject FlagBeingHeld;


    public float currentRadius = 2f;
    public Vector3 center = Vector3.zero;
    // Use this for initialization
    void Start ()
    {
		
	}
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + center , currentRadius);
    }
    // Update is called once per frame
    void Update ()
    {

        if (CrossPlatformInputManager.GetButtonDown("FireP1") )
        {
            if (IsHolding)
            {
                //Drop Flag
                FlagBeingHeld.GetComponent<FlagPickup>().DropFlag();
                IsHolding = false;
            }

            else
            {
                TryingToPickUp = true;
                Collider[] sphereHits;
                sphereHits = Physics.OverlapSphere(transform.position + center, currentRadius);
                foreach (Collider col in sphereHits)
                {
                    FlagPickup p = col.GetComponent<FlagPickup>();
                    if(p!= null)
                    {
                        Debug.Log("yo");

                    }

                  
                }
            }
        }

        if (CrossPlatformInputManager.GetButtonUp("FireP1"))
        {
            TryingToPickUp = false;
        }
		
	}
}
