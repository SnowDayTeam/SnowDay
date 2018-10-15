using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class GlobalCharacterSettings : MonoBehaviour {
 
	public GameObject car1;
	public GameObject car2;

	public ThirdPersonCharacter P1;
	public ThirdPersonCharacter P2;
	public ThirdPersonCharacter P3;
	public ThirdPersonCharacter P4;

	public ReachL reachLP1;
	public ReachR reachRP1;

	public ReachL reachLP2;
    public ReachR reachRP2;

	public ReachL reachLP3;
    public ReachR reachRP3;
    
	public ReachL reachLP4;
    public ReachR reachRP4;

	public float globalSpeedOffset;
	public bool carsOn = true;
    
	float p1SpeedOffset;
	float p2SpeedOffset;
	float p3SpeedOffset;
	float p4SpeedOffset;


	[Range(0.5f, 1.5f)]
    public float globalCharacterSpeed;


	
	// Update is called once per frame
	void Update () {

        //Slow Down Player When in Range of the Ball
		if (reachLP1.isIn || reachRP1.isIn)
        {
            p1SpeedOffset = globalSpeedOffset;
			print(p1SpeedOffset);
        }
        else
        {
            p1SpeedOffset = 0;
        }

        if (reachLP2.isIn || reachRP2.isIn)
        {
            p2SpeedOffset = globalSpeedOffset;
        }
        else
        {
            p2SpeedOffset = 0;
        }

        if (reachLP3.isIn || reachRP3.isIn)
        {
            p3SpeedOffset = globalSpeedOffset;
        }
        else
        {
            p3SpeedOffset = 0;
        }

        if (reachLP4.isIn || reachRP4.isIn)
        {
            p4SpeedOffset = globalSpeedOffset;
        }
        else
        {
            p4SpeedOffset = 0;
        }

		P1.m_MoveSpeedMultiplier = globalCharacterSpeed - p1SpeedOffset;
		P2.m_MoveSpeedMultiplier = globalCharacterSpeed - p2SpeedOffset;
		P3.m_MoveSpeedMultiplier = globalCharacterSpeed - p3SpeedOffset;
		P4.m_MoveSpeedMultiplier = globalCharacterSpeed - p4SpeedOffset;

		P1.m_AnimSpeedMultiplier = globalCharacterSpeed - p1SpeedOffset;
		P2.m_AnimSpeedMultiplier = globalCharacterSpeed - p2SpeedOffset;
		P3.m_AnimSpeedMultiplier = globalCharacterSpeed - p3SpeedOffset;
		P4.m_AnimSpeedMultiplier = globalCharacterSpeed - p4SpeedOffset;

        //Cars
		if (carsOn == false){
			car1.SetActive(false);
			car2.SetActive(false);
		} else {
			car1.SetActive(true);
            car2.SetActive(true);
		}
	}
}
