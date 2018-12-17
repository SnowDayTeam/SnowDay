using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickup : MonoBehaviour {

    public Component[] meshRenderer;
    public bool canBeRetrieved = true;
    public bool isAtBase = false;
    public float timeLeft = 2.0f;
    public int nameChangeCount = 0;

    void Start()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
        gameObject.tag = "Untagged";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RedTeam")
        {
            if (canBeRetrieved && !isAtBase && !other.GetComponent<PlayerScript>().didGetFlag == true)
            {
                
                foreach (MeshRenderer material in meshRenderer)
                        material.material.color = Color.red;

                gameObject.transform.parent = other.transform;
                other.GetComponent<PlayerScript>().didGetFlag = true;
                gameObject.tag = "RedFlag";
                nameChangeCount += 1;
                canBeRetrieved = false;
                }
            
        }
    

        if (other.tag == "BlueTeam")
        {
            if (canBeRetrieved && !isAtBase && !other.GetComponent<PlayerScript>().didGetFlag == true)
            {
                foreach (MeshRenderer material in meshRenderer)
                        material.material.color = Color.blue;

                gameObject.transform.parent = other.transform;
                other.GetComponent<PlayerScript>().didGetFlag = true;

                gameObject.tag = "BlueFlag";
                nameChangeCount += 1;
                canBeRetrieved = false;
            }
        }
    }

  

    void Update()
    {
        if (!canBeRetrieved && !isAtBase)
        {
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft < 0)
        {
            canBeRetrieved = true;
            timeLeft = 2.0f;
        }
    }

}
