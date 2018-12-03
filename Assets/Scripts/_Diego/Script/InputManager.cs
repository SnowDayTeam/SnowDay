using Rewired;
using UnityEngine;
using SnowDay.Diego.Singleton;

namespace SnowDay.Diego.Input
{
    public enum PlayerNumber { P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16 }

    public enum ButtonName { Left_Horizontal, Left_Vertical, Right_Horizontal, Right_Vertical, Left_Bumper, Left_Trigger, Right_Bumper, Right_Trigger, Left_Stick, Right_Stick, A, B, X, Y, Select, Start, DPad_Down, DPad_Up, DPad_Left, DPad_Right }

    public class InputManager : Singleton<InputManager>
    {
        public float GetAxis(PlayerNumber player, string axisName)
        {
            Player rewiredPlayer = ReInput.players.GetPlayer((int)player);
            return rewiredPlayer.GetAxis(axisName);
        }

        public float GetAxis(PlayerNumber player, ButtonName buttonName)
        {
            Player rewiredPlayer = ReInput.players.GetPlayer((int)player);
            return rewiredPlayer.GetAxis(buttonName.ToString());
        }

        public bool GetButton(PlayerNumber player, ButtonName buttonName)
        {
            Player rewiredPlayer = ReInput.players.GetPlayer((int)player);
            return rewiredPlayer.GetButton(buttonName.ToString());
        }

        public bool GetButtonDown(PlayerNumber player, ButtonName buttonName)
        {
            Player rewiredPlayer = ReInput.players.GetPlayer((int)player);
            return rewiredPlayer.GetButton(buttonName.ToString());
        }

        public bool GetButtonUp(PlayerNumber player, ButtonName buttonName)
        {
            Player rewiredPlayer = ReInput.players.GetPlayer((int)player);
            return rewiredPlayer.GetButton(buttonName.ToString());
        }
    }
}