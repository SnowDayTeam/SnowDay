using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PierInputManager : MonoBehaviour
{
    public enum PlayerNumber { P1, P2 ,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16}
    public enum ButtonName { Left_Horizontal,Left_Vertical, Right_Horizontal, Right_Vertical, Left_Bumper, Left_Trigger, Right_Bumper, Right_Trigger, Left_Stick, Right_Stick,A,B,X,Y ,Select,Start }
    public PlayerNumber playerNumber;
    public Player player;
    public void Setup()
    {
        if (player == null)
        {
            player = ReInput.players.GetPlayer((int)playerNumber);

        }
    }
    public void Awake()
    {
        Setup();
    }
    public float GetAxis( string axisName)
    {

        return player.GetAxis(axisName);


    }
    public float GetAxis(ButtonName AxisName)
    {

        return player.GetAxis(AxisName.ToString());


    }

    public bool GetButton( ButtonName buttonName)
    {
      
            return player.GetButton(buttonName.ToString());

    }

    public bool GetButtonDown( ButtonName buttonName)
    {

            return player.GetButtonDown(buttonName.ToString());

  
    }
   
    public  bool GetButtonUp(ButtonName buttonName)
    {
    
            return player.GetButtonUp(buttonName.ToString());

        
    }
}
