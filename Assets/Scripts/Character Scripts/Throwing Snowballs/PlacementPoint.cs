using UnityEngine;
using RootMotion.FinalIK;


public class PlacementPoint : MonoBehaviour {

    private GameObject heldObject;
    private Vector3 newPos;
   // public PlacementManager placementManager;




    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactive")
        {
            heldObject = other.gameObject;
            //newPos = heldObject.transform.position;
            //OnPutDown();
        }


    }


    //Release The object if it touches the placement point trigger while parented to the hand
    //This is called from a message sent by the Interaction Object component of the held object
    void OnPutDown()
    {
        print("Putting Down");

        StopAllCoroutines();
        //If an interactive object is parented to the hand..
        if (heldObject != null && heldObject.transform.parent != null)
        {
            // Release the hand pose
            var poser = heldObject.transform.parent.GetComponent<Poser>();
            if (poser != null)
            {
                poser.poseRoot = null;
                poser.weight = 0f;
            }
            //Reactivate the object's physics
            heldObject.GetComponent<Rigidbody>().isKinematic = false;

            //This lline keeps the object from being offset for some unknown reason
            //heldObject.transform.position = newPos;

            heldObject.GetComponent<ObjectTakeAndLeave>().OnRelease();
            heldObject.transform.position = transform.position;
            heldObject.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
        }
        //release the object from the hand

    }
    }

