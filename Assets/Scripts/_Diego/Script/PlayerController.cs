using RootMotion.Dynamics;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace SnowDay.Diego.CharacterController
{
    [RequireComponent(typeof(PierInputManager))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Third Person Character")]
        public GameObject ThirdPersonCharacter;

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
            SetSnowDayCharacter(PlayerPrefab[currentPrefab]);
        }

        /// <summary>
        /// Gets the player character's current position in wold space.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetCharacterPosition()
        {
            if (!ThirdPersonCharacter)
            {
                return transform.position;
            }
            return ThirdPersonCharacter.transform.position;
        }

        /// <summary>
        /// Gets the third person character script attached to the player characters
        /// </summary>
        /// <returns></returns>
        public SnowDayCharacter GetPlayerCharacter()
        {
            if (!ThirdPersonCharacter)
            {
                ThirdPersonCharacter = Instantiate(PlayerPrefab[currentPrefab], transform);
            }
            playerCharacter = ThirdPersonCharacter.GetComponent<SnowDayCharacter>();
            return playerCharacter;
        }

        public SnowDayCharacter SetSnowDayCharacter(GameObject character)
        {
            Vector3 characterPosition;
            Quaternion characterRotation;
            if (ThirdPersonCharacter)
            {
              characterPosition = ThirdPersonCharacter.transform.position;
              characterRotation = ThirdPersonCharacter.transform.rotation;
              ThirdPersonCharacter = Instantiate(character, characterPosition, characterRotation, transform);
              Destroy(ThirdPersonCharacter);
            }
            else
            {
                ThirdPersonCharacter = Instantiate(character, transform);
            }

            playerCharacter = ThirdPersonCharacter.GetComponent<SnowDayCharacter>();
            playerCharacter.Initialize();
            return playerCharacter;
        }

        private void ChangeCharacterModel(int step)
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

            if (ThirdPersonCharacter)
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