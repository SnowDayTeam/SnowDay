using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using SnowDay.Diego.CharacterController;

public class ShovelHUD : MonoBehaviour
{
    [SerializeField]
    //NEED to find a better way to get player position this is only temporary
    SnowDayCharacter Player;
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
    ShovelController ShovelMaster;
    
   

	// Use this for initialization
	void Start () {
        Player = gameObject.GetComponentInParent<SnowDayCharacter>();

    }
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();
        GetSnowAmount();
	}

    void FollowPlayer()
    {
        if(Player!= null)
            transform.position = new Vector3(Player.transform.position.x+HorizontalOffset, Height, Player.transform.position.z);
    }
    
    void GetSnowAmount()
    {
        float MeterPercent = ShovelMaster.currentsSnowVolume / 100;
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
