using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.CharacterController;


public class SpeedPower : MonoBehaviour {

    private float speed;
    [Range(1,3)]
    public float speedBoostAmount;
    [Range(1,5)]
    public float superSpeedTime;
  

	void Start () {
        //speedBoostAmount = 2.0f;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            //should work for current proper controller
            speed = other.GetComponent<SnowDayCharacter>().m_MoveSpeedMultiplier;
            //used for testing
            //speed = other.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().m_MoveSpeedMultiplier;
            Debug.Log("Boost");
            Invoke("SpeedBoost",0);
        }
    }
    void SpeedBoost() {
        speed *= speedBoostAmount;
        Invoke("NormalSpeed", superSpeedTime);
    }

    void NormalSpeed() {
        speed /= speedBoostAmount;
    }
}
