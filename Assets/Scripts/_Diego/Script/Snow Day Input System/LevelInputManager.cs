using Rewired;
using UnityEngine;
using SnowDay.Diego.Singleton;

namespace SnowDay.Input
{
   
    public class LevelInputManager : Singleton<InputManager>
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
            return rewiredPlayer.GetButtonDown(buttonName.ToString());
        }

        public bool GetButtonUp(PlayerNumber player, ButtonName buttonName)
        {
            Player rewiredPlayer = ReInput.players.GetPlayer((int)player);
            return rewiredPlayer.GetButtonUp(buttonName.ToString());
        }
    }
}