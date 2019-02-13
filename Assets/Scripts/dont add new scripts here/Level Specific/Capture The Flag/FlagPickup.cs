using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickup : MonoBehaviour
{

    public Component[] meshRenderer;
    public bool IsBeingHeld = false;
    public int whoIsHolding;
    public bool isAtBase = false;
    float DropOffset = .05f;
    [HideInInspector]
    public bool CantBeInteractedWith=false;

    float TakeTime = 2;
    float CurrentTakeTime;

    [SerializeField]
    Transform TracePoint;
    [HideInInspector]
    public FlagSpawner spawner;

    //CTFGameManager GM;
    CaptureTheFlagGamemodeManager FlagGamemodeManager = null;

    void Start()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
        this.FlagGamemodeManager = FindObjectOfType<CaptureTheFlagGamemodeManager> ();
        //GM=FindObjectOfType<CTFGameManager>();
        //to check if object refernce will actually work
        Debug.Log("The GM is " + this.FlagGamemodeManager);

    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "RedTeam")
        //{
        //    if (canBeRetrieved && !isAtBase && !other.GetComponent<FlagController>().TryingToPickUp == true)
        //    {

        //        foreach (MeshRenderer material in meshRenderer)
        //            material.material.color = Color.red;

        //        gameObject.transform.parent = other.transform;
        //        other.GetComponent<FlagController>().IsHolding = true;
        //        gameObject.tag = "RedFlag";
        //        nameChangeCount += 1;
        //        canBeRetrieved = false;
        //    }

        ////}
        //Debug.Log(other.GetComponentInChildren<FlagController>());
      
        //// SWITCH TAG TO ENUM WHEN AVAILABLE
        //if (other.GetComponentInChildren<FlagController>())
        //{
            
        //    if (other.GetComponentInChildren<FlagController>().TryingToPickUp)
        //    {
        //        other.GetComponentInChildren<FlagController>().FlagBeingHeld = gameObject;

        //        foreach (MeshRenderer material in meshRenderer)
        //            material.material.color = Color.red;

        //        gameObject.transform.parent = other.transform;
        //        other.GetComponentInChildren<FlagController>().IsHolding = true;
        //        IsBeingHeld = true;


        //    }

        //}


        //    if (other.tag == "BlueTeam")
        //    {
        //        if (canBeRetrieved && !isAtBase && !other.GetComponent<FlagController>().TryingToPickUp == true)
        //        {
        //            foreach (MeshRenderer material in meshRenderer)
        //                    material.material.color = Color.blue;

        //            gameObject.transform.parent = other.transform;
        //            other.GetComponent <FlagController> ().IsHolding = true;

        //            gameObject.tag = "BlueFlag";
        //            nameChangeCount += 1;
        //            canBeRetrieved = false;
        //        }
        //    }
        //}
    }




    //void Update()
    //    {
    //        if (!canBeRetrieved && !isAtBase)
    //        {
    //            timeLeft -= Time.deltaTime;
    //        }

    //        if (timeLeft < 0)
    //        {
    //            canBeRetrieved = true;
    //            timeLeft = 2.0f;
    //        }
    //    }




    public void PickUpFlag(Transform PlayerT, int TeamNum, Transform FlagPosition)
    {
        
        //if player is carrying this Tell them they are no longer holding
        if(transform.parent != null)
        {
            Debug.Log("setholding to null");
            transform.parent.GetComponent<FlagController>().IsHolding = false;
            transform.parent.GetComponent<FlagController>().numberOfFlagsHeld = 0;
            //must add offset due to balance out flags height when transfering from one player to another
            transform.position = new Vector3(transform.position.x, transform.position.y - DropOffset, transform.position.z);


        }

        if (TeamNum == 1)
        {
            foreach (MeshRenderer material in meshRenderer)
                material.material.color = Color.red;
 
        }

        else
        {
            foreach (MeshRenderer material in meshRenderer)
                material.material.color = Color.blue;

           
        }

        whoIsHolding = TeamNum;
        gameObject.transform.parent = PlayerT;
        gameObject.transform.position = new Vector3(FlagPosition.position.x,transform.position.y+DropOffset,FlagPosition.position.z);
        
        IsBeingHeld = true;

    }


    public void DropFlag()
    {
        
        foreach (MeshRenderer material in meshRenderer)
        {
            material.material.color = Color.green;
        }
          

        transform.parent = null;

        transform.position = new Vector3(transform.position.x, transform.position.y - DropOffset, transform.position.z);
        IsBeingHeld = false;

        //Check if being put in goal Zone
        RaycastHit hit;

        Ray ray = new Ray(TracePoint.position, -Vector3.up);
        //Debug.DrawRay(TracePoint.position, -Vector3.up, Color.red, 10);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.gameObject.GetComponent<GoalZone>())
            {
                if (hit.collider.gameObject.GetComponent<GoalZone>().Team == 1)
                {
                    //set all variables and add to red team score
                    Debug.Log("RED GETS A POINT");
                    CantBeInteractedWith = true;
                    spawner.alreadySpawned = false;
                    this.FlagGamemodeManager.CurrentFlags--;
                    this.FlagGamemodeManager.RedTeamScore++;
                    foreach (MeshRenderer material in meshRenderer)
                    {
                        material.material.color = Color.red;
                    }
                }

                else
                {
                    //set all variables and add to blue team score
                    Debug.Log("BLUE GETS A POINT");
                    CantBeInteractedWith = true;
                    spawner.alreadySpawned = false;
                    this.FlagGamemodeManager.CurrentFlags--;
                    this.FlagGamemodeManager.BlueTeamScore++;
                    foreach (MeshRenderer material in meshRenderer)
                    {
                        material.material.color = Color.blue;
                    }
                }
            }
        }




    }

    public void TakeAway()
    {
        
    }

}