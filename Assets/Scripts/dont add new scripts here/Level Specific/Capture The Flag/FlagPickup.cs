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

    float TakeTime = 2;
    float CurrentTakeTime;

    [SerializeField]
    Transform TracePoint;

    void Start()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
      
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




    public void PickUpFlag(Transform PlayerT, int TeamNum)
    {
        //if player is carrying this Tell 
        if(transform.parent != null)
        {
            Debug.Log("setholding to null");
            transform.parent.GetComponent<FlagController>().IsHolding = false;
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
        IsBeingHeld = true;

    }


    public void DropFlag()
    {
        Debug.Log("DROOOP");
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
        Debug.DrawRay(TracePoint.position, -Vector3.up, Color.red, 10);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.gameObject.GetComponent<GoalZone>())
            {
                if (hit.collider.gameObject.GetComponent<GoalZone>().Team == 1)
                {
                    Debug.Log("RED GETS A POINT");
                }

                else
                {
                    Debug.Log("BLUE GETS A POINT");
                }
            }
        }




    }

    public void TakeAway()
    {
        
    }

}