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
        public PierInputManager.ButtonName MeshChangeUp = PierInputManager.ButtonName.DPad_Up;
        public PierInputManager.ButtonName MeshChangeDown = PierInputManager.ButtonName.DPad_Down;
        public List<PlayerController> players = new List<PlayerController>(INPUT_CONSTANTS.Max_Players);

        private LevelInputManager inputController;
        public characterPrefabData prefabs;

        // Use this for initialization
        void Start()
        {
            inputController = GetComponent<LevelInputManager>();
        }
        public void ActivatePlayer(int playerNumber)
        {
            if (players[playerNumber] != null && players[playerNumber].CharacterEnabled == false)
            {
                Debug.Log("Controller Activated" + playerNumber);
                players[playerNumber].CharacterEnabled = true;

                ChangeCharacterModel(players[playerNumber], 1);
            }
        }
        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < INPUT_CONSTANTS.Max_Players; i++)
            {
                bool keyPressed = inputController.GetButtonDown((PlayerNumber)i, (ButtonName)ActivationButton);
                if (keyPressed)
                {
                    ActivatePlayer(i);
                   
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
                keyPressed = inputController.GetButtonDown((PlayerNumber)i, (ButtonName)MeshChangeUp);
                if (keyPressed)
                {
                    ChangeCharacterModel(players[i],1);
                }

                keyPressed = inputController.GetButtonDown((PlayerNumber)i, (ButtonName)MeshChangeDown);
                if (keyPressed)
                {
                    ChangeCharacterModel(players[i] ,- 1);
                }
               
            }

        }
        /// <summary>
        /// Change Character Model
        /// </summary>
        /// <param name="step"></param>
        public void ChangeCharacterModel(PlayerController player, int step)
        {
            player.currentPrefab += step;

            player.currentPrefab = player.currentPrefab < 0 ? prefabs.PrefabList .Length - 1 : player.currentPrefab;

            player.currentPrefab = player.currentPrefab > prefabs.PrefabList.Length - 1 ? 0 : player.currentPrefab;

            player.SetSnowDayCharacter(prefabs.PrefabList[player.currentPrefab]);

            //Debug.Log(currentPrefab);
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
