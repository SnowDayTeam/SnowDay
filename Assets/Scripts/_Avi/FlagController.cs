using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FlagController : MonoBehaviour {

    [SerializeField]
    float RequiredHold = .5f;

    float HoldStarted;
    [HideInInspector]
    public bool TryingToPickUp=false;
    [HideInInspector]
    public bool IsHolding = false;
    //[HideInInspector]
    public int team;
    bool TryingToTake;
    FlagPickup Flag;


    public float currentRadius = 1.25f;
    public Vector3 center = new Vector3(.04f, 1.26f, .03f);

    //InputVariables
    public PierInputManager.ButtonName PickUpFlag = PierInputManager.ButtonName.B;
    public PierInputManager.ButtonName ThrowButton = PierInputManager.ButtonName.Y;

    private PierInputManager playerInputController;

    //Vector3.zero;
    // Use this for initialization
    void Start ()
    {
        playerInputController = gameObject.GetComponentInParent<PierInputManager>();
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + center , currentRadius);
    }
    // Update is called once per frame
    void Update ()
    {

        if (playerInputController.GetButtonDown(PickUpFlag) )
        {
           //if not already holding
            if (IsHolding==false)
            {
               
                Debug.Log("GRAAB");
                Collider[] sphereHits;
                sphereHits = Physics.OverlapSphere(transform.position + center, currentRadius);
                foreach (Collider col in sphereHits)
                {
                    FlagPickup p = col.GetComponent<FlagPickup>();
                    if (p != null)
                    {
                        if (p.transform.parent != null)
                        {
                            Debug.Log("beingheld");
                            //take flag from team mate
                            if (p.whoIsHolding == team)


                            {
                                Flag = p;
                                p.PickUpFlag(gameObject.transform, team);
                                IsHolding = true;
                            }
                            //try to take flag from other team
                            else
                            {
                                Debug.Log("TAKE FLAG");
                                TryingToTake = true;
                                Flag = p;
                                HoldStarted = Time.time;



                            }


                        }
                        //flag is in ground pick up
                        else
                        {
                            Flag = p;
                            p.PickUpFlag(gameObject.transform, team);
                            IsHolding = true;
                        }

                    }

                }
            }

            else
            {

                Flag.DropFlag();
                IsHolding = false;
            }
        }

        if (playerInputController.GetButton(PickUpFlag))
        {
            if (TryingToTake)
            {
                CheckForFlag();
                
            }
        }

        if (playerInputController.GetButtonUp(PickUpFlag))
        {
            TryingToTake = false;
        }
        
		
	}

    void CheckForFlag()
    {

        Collider[] sphereHits;
        sphereHits = Physics.OverlapSphere(transform.position + center, currentRadius);
        foreach (Collider col in sphereHits)
        {
            
            FlagPickup p = col.GetComponent<FlagPickup>();
            if (p == Flag)
            {
                if (Time.time - HoldStarted > RequiredHold)
                {
                    p.PickUpFlag(gameObject.transform, team);
                    IsHolding = true;
                    
                    
                }
            }
        }
    }


    void BeingHeld()
    {
        //effect the player when there flags being taken
    }
}
