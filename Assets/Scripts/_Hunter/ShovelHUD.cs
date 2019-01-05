using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelHUD : MonoBehaviour {
    [SerializeField]
    //NEED to find a better way to get player position this is only temporary
     GameObject Player;
    [SerializeField]
    float Height=1.35f;
    [SerializeField]
    float HorizontalOffset = .18f;
    [SerializeField]
    Image SnowMeter;
    [SerializeField]
    Color Green ;
    [SerializeField]
    Color red;
    [SerializeField]
    Color yellow;
    [SerializeField]
    GameObject ShovelMaster;
    
   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();
        GetSnowAmount();
	}

    void FollowPlayer()
    {
        transform.position = new Vector3(Player.transform.position.x+HorizontalOffset, Height, Player.transform.position.z);
    }
    
    void GetSnowAmount()
    {
        float MeterPercent = ShovelMaster.GetComponent<ShovelController>().currentsSnowVolume / 100;
        SnowMeter.fillAmount = MeterPercent;

        if (MeterPercent < .4)
        {
            SnowMeter.color = Green;
        }

        else if(MeterPercent > .4)
        {
            SnowMeter.color = yellow;
        }
        if (MeterPercent > .6)
        {
            SnowMeter.color = red;
        }


    }
}
