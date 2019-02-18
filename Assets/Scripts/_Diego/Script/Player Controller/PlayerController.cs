using RootMotion.Dynamics;
using UnityEngine;
using SnowDay.Input;

namespace SnowDay.Diego.CharacterController
{
    [RequireComponent(typeof(PlayerInputController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Third Person Character")]
        public GameObject SnowDayCharacterGameObject;

        // [Header("Puppet Master")]
        public PuppetMaster puppetMaster;

        [Header("Key Bindings")]
        public PierInputManager.ButtonName HorizontalAxis = PierInputManager.ButtonName.Left_Horizontal;
        public PierInputManager.ButtonName VerticalAxis = PierInputManager.ButtonName.Left_Vertical;
        public PierInputManager.ButtonName CrouchKey = PierInputManager.ButtonName.Left_Bumper;
        public PierInputManager.ButtonName JumpKey = PierInputManager.ButtonName.A;
        //public PierInputManager.ButtonName RunKey = PierInputManager.ButtonName.B;


        private PierInputManager playerInputController;
        private SnowDayCharacter playerCharacter;

        [Header("Debug")]
        public int currentPrefab = 0;
        public Transform playerIndicator;
      //  public GameObject[] PlayerPrefab;

        private Transform m_Cam;
        private Vector3 m_Move;
        private bool m_Jump;
        private bool crouch;
        private float horizontalAxis;
        private float verticalAxis;
        public float RunBoostMultiplier = 1.5f;

        private bool activeSelf = false;
        public bool CharacterEnabled//bad design hides intent 
        {
            get
            {
                return activeSelf;
            }
            set
            {
                activeSelf = value;
              //  playerCharacter.Active = value;
            }
        }
        public PierInputManager GetInputController()
        {
            return playerInputController;
        }
        // Use this for initialization
        private void Start()
        {
            playerInputController = gameObject.GetComponent<PierInputManager>();
            playerIndicator.gameObject.SetActive(false);
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            //if (!PuppetMaster)
            //{
            //    Debug.Log("Missing Puppet Master Component!");
            //}
          //  SetSnowDayCharacter(PlayerPrefab[currentPrefab], false);
        
        }
        public void MoveCharacter(Vector3 position)
        {
            puppetMaster.mode = PuppetMaster.Mode.Disabled ;
            playerCharacter.transform.position = position;
        //   Debug.Log("puppet disabled");
            Invoke("ActivatePuppet", 5);
        }

        public void ActivatePuppet()
        {
           // Debug.Log(puppetMaster.mode);
            puppetMaster.mode = PuppetMaster.Mode.Active;

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
            if (!SnowDayCharacterGameObject)
            {
                return transform.position;
            }
            return playerCharacter.transform.position;
        }

        /// <summary>
        /// Gets the Snow person character script attached to the player characters
        /// </summary>
        /// <returns></returns>
        public SnowDayCharacter GetPlayerCharacter()
        {
            //if (!SnowDayCharacterGameObject)
            //{
            //    SnowDayCharacterGameObject = Instantiate(PlayerPrefab[currentPrefab], transform);
            //}

            if(SnowDayCharacterGameObject != null)
            {
                playerCharacter = SnowDayCharacterGameObject.GetComponentInChildren<SnowDayCharacter>();
                return playerCharacter;
            }
            return null;
        }

        /// <summary>
        /// Sets the snow day character
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public SnowDayCharacter SetSnowDayCharacter(GameObject character)
        {
            Vector3 characterPosition;
            Quaternion characterRotation;
            playerIndicator.gameObject.SetActive(true);

            if (SnowDayCharacterGameObject != null && playerCharacter != null)// explicitly check for null 
            {
                characterPosition = playerCharacter.transform.position;
                characterRotation = playerCharacter.transform.rotation;
                Destroy(SnowDayCharacterGameObject);
                SnowDayCharacterGameObject = Instantiate(character, characterPosition, characterRotation, transform);
            }
            else
            {
                SnowDayCharacterGameObject = Instantiate(character, transform);
            }

            playerCharacter = SnowDayCharacterGameObject.GetComponentInChildren<SnowDayCharacter>();
            playerCharacter.Initialize();
            puppetMaster = null;// gameObject.GetComponentInChildren<PuppetMaster>();
            puppetMaster = SnowDayCharacterGameObject.GetComponentInChildren<PuppetMaster>();

            Debug.Log(puppetMaster.gameObject);
            puppetMaster.mode = PuppetMaster.Mode.Kinematic;

            playerIndicator.GetComponentInChildren<SpriteRenderer>().color= SnowDayCharacterGameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].GetColor("_TeamColor");
            playerIndicator.GetComponentInChildren<TextMesh>().text = playerInputController.playerNumber.ToString();
            playerIndicator.SetAsLastSibling();// hack to change 
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
            Debug.Log("other setCharacter");
            return playerCharacter;
        }

       

        // Update is called once per frame
        private void Update()
        {
            horizontalAxis = playerInputController.GetAxis(HorizontalAxis);
            verticalAxis = playerInputController.GetAxis(VerticalAxis);

            crouch = playerInputController.GetButton(CrouchKey);

          

            if (SnowDayCharacterGameObject)
            {
                playerIndicator.transform.position = playerCharacter.transform.position +Vector3.up *2.5f;
                if (!m_Jump)
                {
                    m_Jump = playerInputController.GetButtonDown(JumpKey);
                }

                if (Camera.main != null)
                {
                    // calculate camera relative direction to move:
                    Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                    m_Move = verticalAxis * m_CamForward + horizontalAxis * Camera.main.transform.right;
                }
                else
                {
                    m_Move = verticalAxis * Vector3.forward + horizontalAxis * Vector3.right;
                }

#if !MOBILE_INPUT
                // walk speed multiplier
              //  if (playerInputController.GetButton(RunKey)) m_Move *= RunBoostMultiplier;
#endif

                playerCharacter.Move(m_Move, crouch, m_Jump);

                m_Jump = false;
            }
        }
    }
}