using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Input;
using SnowDay.Diego.CharacterController;



namespace SnowDay.Diego.LevelSelect
{
    [RequireComponent(typeof(LevelInputManager))]
    public class LevelSelectPlayerActivator : MonoBehaviour
    {
        public PierInputManager.ButtonName ActivationButton = PierInputManager.ButtonName.A;
        public PierInputManager.ButtonName RemoveButton = PierInputManager.ButtonName.Start;

        public List<PlayerController> players = new List<PlayerController>(INPUT_CONSTANTS.Max_Players);

        private LevelInputManager inputController;


        // Use this for initialization
        void Start()
        {
            inputController = GetComponent<LevelInputManager>();
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < INPUT_CONSTANTS.Max_Players; i++)
            {
                bool keyPressed = inputController.GetButtonDown((PlayerNumber)i, (ButtonName)ActivationButton);
                if (keyPressed)
                {
                    Debug.Log("Controller Activated" + i);
                   if (players[i] != null)
                   {
                       players[i].CharacterEnabled = true;
                   }
                }

                keyPressed = inputController.GetButtonDown((PlayerNumber)i, (ButtonName)RemoveButton);
                if (keyPressed)
                {
                    Debug.Log("Controller Activated" + i);
                    if (players[i] != null)
                    {
                        players[i].CharacterEnabled = false;
                    }
                }
            }

        }

        /// <summary>
        /// Gets a list of the active players in the scene
        /// </summary>
        /// <returns></returns>
        public List<PlayerController> GetActivePlayers()
        {
            List<PlayerController> activePlayers = new List<PlayerController>();
            for (int i = 0; i < INPUT_CONSTANTS.Max_Players; i++)
            {
                if (players[i] != null)
                {
                    if (players[i].CharacterEnabled)
                    {
                        activePlayers.Add(players[i]);
                    }
                }
            }
            return activePlayers;
        }
    } 
}
