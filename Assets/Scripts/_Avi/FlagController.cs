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
    //For when trying to take from other team
    Rigidbody FlagHolder;

    //Spheretrace variables
    public float currentRadius;
    public Vector3 center = new Vector3(.04f, 1.26f, .03f);

    //InputVariables
    public PierInputManager.ButtonName PickUpFlag = PierInputManager.ButtonName.B;
    public PierInputManager.ButtonName ThrowButton = PierInputManager.ButtonName.Y;
    private PierInputManager playerInputController;

    [HideInInspector]
    public int numberOfFlagsHeld = 0;


    //postion the flag will go when picked up
    [SerializeField]
    Transform FlagHoldPostion;


    void Start ()
    {
        playerInputController = gameObject.GetComponentInParent<PierInputManager>();
        currentRadius = 0.5f;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + center , currentRadius);
    }
    // Update is called once per frame
    void Update ()
    {
        //on flag take button press
        if (playerInputController.GetButtonDown(PickUpFlag) )
        {
           //if not already holding
            if (IsHolding==false)
            {
               
                Collider[] sphereHits;
                sphereHits = Physics.OverlapSphere(transform.position + center, currentRadius);
                foreach (Collider col in sphereHits)
                {
                    numberOfFlagsHeld++;

                    FlagPickup p = col.GetComponent<FlagPickup>();

                    if (p != null && !IsHolding)
                    {
                        //checks if flag was already planted in a goal
                        if (p.GetComponent<FlagPickup>().CantBeInteractedWith == false)
                        {
                            Debug.Log("Picking Up");

                            Flag = p;
                            p.PickUpFlag(gameObject.transform, team, FlagHoldPostion);
                            IsHolding = true;

                        }

                        /*

                        if (numberOfFlagsHeld == 1)
                        {
                            if (p.transform.parent != null)
                            {
                                Debug.Log("Check 2");

                                //take flag from team mate
                                if (p.whoIsHolding == team)
                                {
                                    Debug.Log("Check 3");

                                    Flag = p;
                                    p.PickUpFlag(gameObject.transform, team, FlagHoldPostion);
                                    IsHolding = true;

                                }
                                //try to take flag from other team
                                else
                                {

                                    if (numberOfFlagsHeld == 2)
                                    {
                                        Debug.Log("Check 4");

                                        TryingToTake = true;
                                        Flag = p;
                                        HoldStarted = Time.time;
                                        //If the amount of time to steal flag has been met

                                        Flag.PickUpFlag(transform, team, FlagHoldPostion);
                                        IsHolding = true;

                                    }
                                }
                            }
                            */
                    }
                        //flag is in ground pick up
                        else
                        {
                            

                            //    }

                       // }
                        }
                }
            }
            //Drop Flag if already holding
            else
            {

                Flag.DropFlag();
                numberOfFlagsHeld = 0;
                IsHolding = false;
            }
        }


        //When flag pickup is held down
        if (playerInputController.GetButton(PickUpFlag))
        {
            if (TryingToTake)
            {
           //     CheckForFlag();
                
            }
        }
        //on button release
        if (playerInputController.GetButtonUp(PickUpFlag))
        {
            if (TryingToTake)
            {
                TryingToTake = false;
                Flag.GetComponentInParent<FlagController>().ModifyMovement(false);
                //set drag to normal
            }        
        }	
	}

  /*  void CheckForFlag()
    {
        //bool FoundFlag = false;
        Debug.Log("Check For Flag");
        Collider[] sphereHits;
        sphereHits = Physics.OverlapSphere(transform.position + center, currentRadius);
        foreach (Collider col in sphereHits)
        {
            FlagPickup F = col.GetComponent<FlagPickup>();
            if (F == Flag)
            {
                //If the amount of time to steal flag has been met
                if (Time.time - HoldStarted > RequiredHold)
                {
                    //Set the person your taking from movment to normal and break
                    Flag.GetComponentInParent<FlagController>().ModifyMovement(false);
                    Flag.PickUpFlag(transform, team, FlagHoldPostion);
                    TryingToTake = false;
                    IsHolding = true;
                    break;
                }
                // modify the other players movment
                else
                {
                    Flag.GetComponentInParent<FlagController>().ModifyMovement(true);
                    break;
                }
            }
        }
        //flagfound
        //if (FoundFlag == true)
        //{
        //    TryingToTake = true;
            
            
        //}
        ////flag not found
        //else
        //{
        //    TryingToTake = false;
           
        //}
    }
    */

    public void ModifyMovement(bool Slowdown)
    {
        if (Slowdown)
        {
            GetComponentInParent<Rigidbody>().drag = 200;
            //Debug.Log("slow down "+gameObject);

        }

        else
        {
            GetComponentInParent<Rigidbody>().drag = 0;
            //Debug.Log("Set " +gameObject+ "back to normal");
        }

    }
 
}
