using Rewired;
using UnityEngine;

namespace SnowDay.Input
{
    public class PlayerInputController : MonoBehaviour
    {
        public PlayerNumber playerNumber;

        private Player player;

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

        public float GetAxis(string axisName)
        {
            return player.GetAxis(axisName);
        }

        public float GetAxis(ButtonName AxisName)
        {
            return player.GetAxis(AxisName.ToString());
        }

        public bool GetButton(ButtonName buttonName)
        {
            return player.GetButton(buttonName.ToString());
        }

        public bool GetButtonDown(ButtonName buttonName)
        {
            return player.GetButtonDown(buttonName.ToString());
        }

        public bool GetButtonUp(ButtonName buttonName)
        {
            return player.GetButtonUp(buttonName.ToString());
        }
    }
}