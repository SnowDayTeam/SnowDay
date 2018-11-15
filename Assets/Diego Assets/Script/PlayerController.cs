using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : PlayerActor
{
    [Header("Player Attributes")]
    [SerializeField] private float m_PlayerSpeed = 10;

    [SerializeField] private float m_MovingTurnSpeed = 360;
    [SerializeField] private float m_StationaryTurnSpeed = 180;

    [Header("Bullet Modes")]
    public ProjectileAttribs bulletModeA;

    public ProjectileAttribs bulletModeB;

    [Header("Components")]
    public ProjectileLauncher launcher;

    private Rigidbody m_Rigidbody;

    // Use this for initialization
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal P1");
        float v = CrossPlatformInputManager.GetAxis("Vertical P1");

        Move(h, v);

        if (Input.GetButtonDown("FireP1"))
        {
            launcher.LaunchProjectile(bulletModeA);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            launcher.LaunchProjectile(bulletModeB);
        }
    }

    private void Move(float horizontalAxis, float verticalAxis)
    {
        //Player Movement
        Vector3 moveVector = new Vector3(horizontalAxis * m_PlayerSpeed, 0, verticalAxis * m_PlayerSpeed);
        m_Rigidbody.velocity = moveVector;

        //Player Rotation
        moveVector = transform.InverseTransformDirection(moveVector);

        float m_ForwardAmount = moveVector.z;
        float turnAmount = Mathf.Atan2(moveVector.x, moveVector.z);
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);

        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }
}