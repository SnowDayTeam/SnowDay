using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickup : MonoBehaviour
{

    public Component[] meshRenderer;
    public bool IsBeingHeld = true;
    public bool isAtBase = false;
    public float timeLeft = 2.0f;
    public int nameChangeCount = 0;
    float DropOffset = .05f;

    void Start()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
        gameObject.tag = "Untagged";
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

        //}
        Debug.Log(other.GetComponentInChildren<FlagController>());
      
        // SWITCH TAG TO ENUM WHEN AVAILABLE
        if (other.GetComponentInChildren<FlagController>())
        {
            
            if (other.GetComponentInChildren<FlagController>().TryingToPickUp)
            {
                other.GetComponentInChildren<FlagController>().FlagBeingHeld = gameObject;

                foreach (MeshRenderer material in meshRenderer)
                    material.material.color = Color.red;

                gameObject.transform.parent = other.transform;
                other.GetComponentInChildren<FlagController>().IsHolding = true;
                IsBeingHeld = true;


            }

        }


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

    public void DropFlag()
    {
        foreach (MeshRenderer material in meshRenderer)
            material.material.color = Color.green;
        transform.parent = null;

        transform.position = new Vector3(transform.position.x, transform.position.y - DropOffset, transform.position.z);
    }

}