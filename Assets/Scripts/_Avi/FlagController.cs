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
    public float currentRadius = 1.25f;
    public Vector3 center = new Vector3(.04f, 1.26f, .03f);

    //InputVariables
    public PierInputManager.ButtonName PickUpFlag = PierInputManager.ButtonName.B;
    public PierInputManager.ButtonName ThrowButton = PierInputManager.ButtonName.Y;
    private PierInputManager playerInputController;

    //postion the flag will go when picked up
    [SerializeField]
    Transform FlagHoldPostion;


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
                    FlagPickup p = col.GetComponent<FlagPickup>();
                    if (p != null)
                    {
                        if (p.transform.parent != null)
                        {
                            
                            //take flag from team mate
                            if (p.whoIsHolding == team)


                            {
                                Flag = p;
                                p.PickUpFlag(gameObject.transform, team,FlagHoldPostion);
                                IsHolding = true;
                            }
                            //try to take flag from other team
                            else
                            {
                         
                                TryingToTake = true;
                                Flag = p;
                                HoldStarted = Time.time;
                               
                               


                            }


                        }
                        //flag is in ground pick up
                        else
                        {
                            //checks if flag was already planted in a goal
                            if (p.GetComponent<FlagPickup>().CantBeInteractedWith == false)
                            {
                                Flag = p;
                                p.PickUpFlag(gameObject.transform, team, FlagHoldPostion);
                                IsHolding = true;
                            }
                           
                        }

                    }

                }
            }
            //Drop Flag if already holding
            else
            {

                Flag.DropFlag();
                IsHolding = false;
            }
        }


        //When flag pickup is held down
        if (playerInputController.GetButton(PickUpFlag))
        {
            if (TryingToTake)
            {
                CheckForFlag();
                
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

    void CheckForFlag()
    {
        //bool FoundFlag = false;

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
