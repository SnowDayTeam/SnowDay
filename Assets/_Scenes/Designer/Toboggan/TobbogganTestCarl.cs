using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;

public class TobbogganTestCarl : MonoBehaviour
{
    public UnityEvent unityEvent;
    public PierInputManager manager;
    public Transform sled;
    public float velTurnLimiter = 500;
    float userForceInput;
    [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
    [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
    [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
    [SerializeField] private float m_JumpPower = 2; // The force added to the ball when it jumps.

    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    private Rigidbody m_Rigidbody;
    private Vector3 move;
    // the world-relative desired move direction, calculated from the camForward and user input.

    private Transform cam; // A reference to the main camera in the scenes transform
    private Vector3 camForward; // The current forward direction of the camera
    private bool jump; // whether the jump button is currently pressed


    private void Awake()
    {
        // Set up the reference.


        // get the transform of the main camera
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
        }
    }

    private void Start()
    {
        sled = transform.parent.GetChild(0);
        m_Rigidbody = GetComponent<Rigidbody>();
        // Set the maximum angular velocity.
        GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
    }

    private void Update()
    {
        // Get the axis and jump input.
       

        float h = manager.GetAxis(PierInputManager.ButtonName.Left_Horizontal);
        float v = manager.GetAxis(PierInputManager.ButtonName.Left_Vertical);
        jump = manager.GetButton(PierInputManager.ButtonName.A);

        if (h > v) {
            userForceInput = Mathf.Abs(h);
        }else{
            userForceInput = Mathf.Abs(v);
        }

        // calculate move direction
        if (cam != null)
        {
            // calculate camera relative direction to move:
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            move = (v * camForward + h * cam.right).normalized;
            Debug.DrawRay(transform.position, move * 5, Color.blue);
            Debug.DrawRay(transform.position, sled.right * 5, Color.red);
            //print(Vector3.Angle(sled.right, move));
            print(m_Rigidbody.velocity.magnitude);

        }
        else
        {
            // we use world-relative directions in the case of no main camera
            move = (v * Vector3.forward + h * Vector3.right).normalized;
        }
    }


    private void FixedUpdate()
    {
        // Call the Move function of the ball controller
        Move(move, jump);
        jump = false;
    }
    public void Move(Vector3 moveDirection, bool jump)
    {
        // If using torque to rotate the ball...
        if (m_UseTorque)
        {

            if(Vector3.Angle(sled.right, move) < 90){
                m_Rigidbody.AddTorque(sled.right * (m_MovePower * userForceInput * (m_Rigidbody.velocity.magnitude/velTurnLimiter)));
                Debug.DrawRay(transform.position, sled.right * (m_MovePower * userForceInput) * 10, Color.yellow);
            } else {
                m_Rigidbody.AddTorque(sled.right * (-m_MovePower * userForceInput * (m_Rigidbody.velocity.magnitude/velTurnLimiter)));
                Debug.DrawRay(transform.position, sled.right * (-m_MovePower * userForceInput) * 10, Color.yellow);


            }

            // ... add torque around the axis defined by the move direction.
            // m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * m_MovePower);



        }
        else
        {
            // Otherwise add force in the move direction.
            if (Vector3.Angle(sled.right, move) < 90)
            {
                m_Rigidbody.AddForce(sled.right * (m_MovePower * userForceInput * (m_Rigidbody.velocity.magnitude/velTurnLimiter)));
                Debug.DrawRay(transform.position, sled.right * (m_MovePower * userForceInput) * 10, Color.yellow);

            }
            else
            {
                m_Rigidbody.AddForce(sled.right * (-m_MovePower * userForceInput * (m_Rigidbody.velocity.magnitude/velTurnLimiter)));
                Debug.DrawRay(transform.position, sled.right * (-m_MovePower * userForceInput) * 10, Color.yellow);


            }
        }

        // If on the ground and jump is pressed...
        if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
        {
            // ... add force in upwards.
            unityEvent.Invoke();
            m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
        }
    }
}

