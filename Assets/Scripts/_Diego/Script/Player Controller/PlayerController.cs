using RootMotion.Dynamics;
using UnityEngine;
using SnowDay.Input;

namespace SnowDay.Diego.CharacterController
{
    [RequireComponent(typeof(PlayerInputController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Third Person Character")]
        public GameObject SnowDayCharacter;

        [Header("Puppet Master")]
        public PuppetMaster PuppetMaster;

        [Header("Key Bindings")]
        public PierInputManager.ButtonName HorizontalAxis = PierInputManager.ButtonName.Left_Horizontal;
        public PierInputManager.ButtonName VerticalAxis = PierInputManager.ButtonName.Left_Vertical;
        public PierInputManager.ButtonName CrouchKey = PierInputManager.ButtonName.Left_Bumper;
        public PierInputManager.ButtonName JumpKey = PierInputManager.ButtonName.A;
        public PierInputManager.ButtonName RunKey = PierInputManager.ButtonName.B;
        public PierInputManager.ButtonName MeshChangeUp = PierInputManager.ButtonName.DPad_Up;
        public PierInputManager.ButtonName MeshChangeDown = PierInputManager.ButtonName.DPad_Down;

        private PierInputManager playerInputController;
        private SnowDayCharacter playerCharacter;

        [Header("Debug")]
        private int currentPrefab = 0;

        public GameObject[] PlayerPrefab;

        private Transform m_Cam;
        private Vector3 m_Move;
        private bool m_Jump;
        private bool crouch;
        private float horizontalAxis;
        private float verticalAxis;

        private bool activeSelf = false;
        public bool CharacterEnabled
        {
            get
            {
                return activeSelf;
            }
            set
            {
                activeSelf = value;
                playerCharacter.Active = value;
            }
        }

        // Use this for initialization
        private void Start()
        {
            playerInputController = gameObject.GetComponent<PierInputManager>();

            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            if (!PuppetMaster)
            {
                Debug.Log("Missing Puppet Master Component!");
            }
            SetSnowDayCharacter(PlayerPrefab[currentPrefab], false);
        
        }

        /// <summary>
        /// Get Active Player ID
        /// </summary>
        /// <returns></returns>
        public int GetControllerID()
        {
            return (int)playerInputController.playerNumber;
        }

        /// <summary>
        /// Gets the player character's current position in wold space.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetCharacterPosition()
        {
            if (!SnowDayCharacter)
            {
                return transform.position;
            }
            return SnowDayCharacter.transform.position;
        }

        /// <summary>
        /// Gets the Snow person character script attached to the player characters
        /// </summary>
        /// <returns></returns>
        public SnowDayCharacter GetPlayerCharacter()
        {
            if (!SnowDayCharacter)
            {
                SnowDayCharacter = Instantiate(PlayerPrefab[currentPrefab], transform);
            }
            playerCharacter = SnowDayCharacter.GetComponent<SnowDayCharacter>();
            return playerCharacter;
        }

        /// <summary>
        /// Sets the snow day character
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        private SnowDayCharacter SetSnowDayCharacter(GameObject character)
        {
            Vector3 characterPosition;
            Quaternion characterRotation;

            if (SnowDayCharacter)
            {
                characterPosition = SnowDayCharacter.transform.position;
                characterRotation = SnowDayCharacter.transform.rotation;
                Destroy(SnowDayCharacter);
                SnowDayCharacter = Instantiate(character, characterPosition, characterRotation, transform);
            }
            else
            {
                SnowDayCharacter = Instantiate(character, transform);
            }

            playerCharacter = SnowDayCharacter.GetComponent<SnowDayCharacter>();
            playerCharacter.Initialize();
            return playerCharacter;
        }

        /// <summary>
        /// Set Snow day character and whether its currently active in the scene
        /// </summary>
        /// <param name="character"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        private SnowDayCharacter SetSnowDayCharacter(GameObject character, bool enabled)
        {
            SetSnowDayCharacter(character);
            playerCharacter.Active = enabled;
            return playerCharacter;
        }

        /// <summary>
        /// Change Character Model
        /// </summary>
        /// <param name="step"></param>
        public void ChangeCharacterModel(int step)
        {
            currentPrefab += step;

            currentPrefab = currentPrefab < 0 ? PlayerPrefab.Length - 1 : currentPrefab;

            currentPrefab = currentPrefab > PlayerPrefab.Length - 1 ? 0 : currentPrefab;

            SetSnowDayCharacter(PlayerPrefab[currentPrefab]);

            Debug.Log(currentPrefab);
        }

        // Update is called once per frame
        private void Update()
        {
            horizontalAxis = playerInputController.GetAxis(HorizontalAxis);
            verticalAxis = playerInputController.GetAxis(VerticalAxis);

            crouch = playerInputController.GetButton(CrouchKey);

            if (playerInputController.GetButtonDown(MeshChangeUp))
            {
                ChangeCharacterModel(1);
            }

            if (playerInputController.GetButtonDown(MeshChangeDown))
            {
                ChangeCharacterModel(-1);
            }

            if (SnowDayCharacter)
            {
                if (!m_Jump)
                {
                    m_Jump = playerInputController.GetButtonDown(JumpKey);
                }

                if (m_Cam != null)
                {
                    // calculate camera relative direction to move:
                    Vector3 m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                    m_Move = verticalAxis * m_CamForward + horizontalAxis * m_Cam.right;
                }
                else
                {
                    m_Move = verticalAxis * Vector3.forward + horizontalAxis * Vector3.right;
                }

#if !MOBILE_INPUT
                // walk speed multiplier
                if (playerInputController.GetButton(RunKey)) m_Move *= 0.5f;
#endif

                playerCharacter.Move(m_Move, crouch, m_Jump);

                m_Jump = false;
            }
        }
    }
}