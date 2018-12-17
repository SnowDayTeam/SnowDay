using UnityEngine;



	public class ObjectTakeAndLeave : MonoBehaviour {

		public Transform leftHand;
		public Transform rightHand;
        public GameObject placementPoint;

        bool holdingRight;
        bool holdingLeft;

    // Called by the Interaction Object
    void OnPickUp()
        {
            StopAllCoroutines();
            //Reset Object after time (debugging)
            //StartCoroutine(ResetObject(Time.time + resetDelay));
        }

    void Holding(){

            //If the item is in the character's Left Hand
            if (gameObject.transform.parent == leftHand)
            {
                //Make the reachpoint active for player's Left Hand
                print("Holding Left");
                holdingLeft = true;
               // placementManager.CanPlaceL();
            
            }
            //If the item is in the character's Right Hand
            if (gameObject.transform.parent == rightHand)
            {
                //Make the reachpoint active for player's Right Hand
                print("Holding Right");
                holdingRight = true;
               // placementManager.CanPlaceR();
            }

            if (holdingLeft == true || holdingRight == true)
            {
                placementPoint.SetActive(true);
            }

        }    		

   public void OnRelease(){
        if (gameObject.transform.parent == leftHand)
        {
            print("Releasing Left");
            holdingLeft = false;
            // placementManager.CantPlaceL();
        }

        if (gameObject.transform.parent == rightHand)
        {
            print("Releasing Right");
            holdingRight = false;
            //placementManager.CantPlaceR();
        }

        if (holdingLeft == false && holdingRight == false){
            placementPoint.SetActive(false);
            print("Disabling Placement Point");
        }

        transform.parent = null;
    }
}
	

