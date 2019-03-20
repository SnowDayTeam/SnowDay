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

        [SerializeField] characterPrefabData prefabs;

        /// <summary>
        /// Represents the list of character that have no yet been chosen
        /// </summary>
        [SerializeField] List<GameObject> TakenCharacters = new List<GameObject> ();
        [SerializeField] int CharacterPrefabIndex = 0;
        //[SerializeField] int NextSelectableCharacter = 0;
        //[SerializeField] int PreviousSelectableCharacter = 0;

        // Use this for initialization
        void Start()
        {
            inputController = GetComponent<LevelInputManager>();
            //this.NextSelectableCharacter = 0;
            //this.PreviousSelectableCharacter = this.SelectableCharacters.Count -1;
        }

        public void ActivatePlayer(int playerNumber)
        {
            if (players[playerNumber] != null && players[playerNumber].CharacterEnabled == false)
            {
                Debug.Log("Controller Activated" + playerNumber);
                players[playerNumber].CharacterEnabled = true;

                ChangeCharacterModel(players[playerNumber], false);
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
                        //enable the commented code when this actually removes the player from the game
                        players[i].CharacterEnabled = false;
                        //remove character from taken list and set its prefab int to default value
                        //this.TakenCharacters.Remove(this.prefabs.PrefabList[players[i].currentPrefab]);
                        //players[i].currentPrefab = -1;
                    }
                }
                keyPressed = inputController.GetButtonDown((PlayerNumber)i, (ButtonName)MeshChangeUp);
                if (keyPressed)
                {
                    ChangeCharacterModel(players[i], false);
                }

                keyPressed = inputController.GetButtonDown((PlayerNumber)i, (ButtonName)MeshChangeDown);
                if (keyPressed)
                {
                    ChangeCharacterModel(players[i], true);
                }
            }
        }

        /// <summary>
        /// Change Character Model
        /// </summary>
        /// <param name="step"></param>
        void ChangeCharacterModel(PlayerController player, bool WantsPreviousCharacter)
        {
            //all characters are taken leave function
            if(this.TakenCharacters.Count == this.prefabs.PrefabList.Length)
                return;

            //add break case if all character are taken
            while(this.TakenCharacters.Contains(this.prefabs.PrefabList[this.CharacterPrefabIndex])) 
            {
                if(WantsPreviousCharacter)
                    this.CharacterPrefabIndex += this.prefabs.PrefabList.Length - 1;
                else
                    this.CharacterPrefabIndex++;
                this.CharacterPrefabIndex %= this.prefabs.PrefabList.Length;
            }

            if(player.SnowDayCharacterGameObject) 
                this.TakenCharacters.Remove(this.prefabs.PrefabList[player.currentPrefab]);

            player.SetSnowDayCharacter(prefabs.PrefabList[this.CharacterPrefabIndex]);

            this.TakenCharacters.Add(this.prefabs.PrefabList[this.CharacterPrefabIndex]);

            player.currentPrefab = this.CharacterPrefabIndex;
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
