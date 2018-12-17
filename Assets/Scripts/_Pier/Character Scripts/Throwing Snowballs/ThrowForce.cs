
using UnityEngine;
using RootMotion.FinalIK;

public class ThrowForce : MonoBehaviour
{


    Rigidbody rb;
    public Transform target;
    public float releaseSpeed = 1;
    public float initialAngle;
    public float headSpeed;

    public FullBodyBipedIK character;
    public AimIK headLook;
    public Transform leftHand;
    public Transform rightHand;
    public bool throwOverhead;

    public Transform overhandThrowMarkerL;
    public Transform overhandShoulderMarkerL;


    ObjectTakeAndLeave objectTakeAndLeave;
   // TargetMover targetMover;

    public float overhandlerpTime = 1f;
    public float currentLerpTime = 1;



    float t = 0.0f;
    float storeLeftHandPullWeight;
    float storeRightHandPullWeight;

    bool isThrownLeft;
    bool isThrownRight;

    float startHandWeightL;
    float startHandRotWeightL;
    float startRelHandPosWeightL;
    float startShoulderWeightL;
    float startPullL;
    float startReachL;
    float pushParentL;
    float bendGoalWeightL;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectTakeAndLeave = GetComponent<ObjectTakeAndLeave>();
        //targetMover = target.gameObject.GetComponent<TargetMover>();

    }
    void LateUpdate()
    {
        if(Input.GetKeyDown("p")){
            throwOverhead = true;
        }

        if (throwOverhead == true)
        {
            WindUp();
        }

        //Enable Moving throw target when we hold L1
        if (Input.GetButton("L1"))
        {
            target.transform.parent = null;
            target.gameObject.SetActive(true);

            if (Input.GetButton("R2"))
            {
                //Launch the thing from the Left Hand
                if (transform.parent == leftHand)
                {
                    objectTakeAndLeave.OnRelease();
                    rb.isKinematic = false;
                    isThrownLeft = true;
                    ObjectForce();
                    character.solver.leftHandEffector.positionWeight = 1f;
                    storeLeftHandPullWeight = character.solver.leftArmChain.pull;
                    character.solver.leftArmChain.pull = 0.3f;
                }
                //Launch the thing from the Right Hand
                if (transform.parent == rightHand)
                {
                    objectTakeAndLeave.OnRelease();
                    rb.isKinematic = false;
                    isThrownRight = true;
                    ObjectForce();
                    character.solver.rightHandEffector.positionWeight = 1f;
                    storeRightHandPullWeight = character.solver.rightArmChain.pull;
                    character.solver.rightArmChain.pull = 0.3f;
                }
            }

            //If this object is being presently being thrown with the LEFT Hand, let the hand follow and release...
            if (isThrownLeft == true)
            {
                //lerp down hand effector weight as we release
                if (character.solver.leftHandEffector.positionWeight > 0)
                {
                    character.solver.leftHandEffector.target = transform;
                    character.solver.leftHandEffector.positionWeight = Mathf.Lerp(1f, 0, t);
                    print("throwing");
                    //Look where we are throwing
                    //headLook.solver.target = target;
                    //headLook.solver.IKPositionWeight = Mathf.Lerp(0, 1.0f, t);
                    t += releaseSpeed * Time.deltaTime;
                }

                //Once the hand has returned to it's natural position...
                if (character.solver.leftHandEffector.positionWeight == 0)
                {
                    //Restore settings and lerp the head back to it's original position
                    t = 0.0f;
                    character.solver.leftArmChain.pull = storeLeftHandPullWeight;
                    character.solver.leftHandEffector.target = null;
                    isThrownLeft = false;

                    //HeadLerpOut();    //<---- TODO Fix this funtion to reset head position
                }
            }

            //If this object is being presently being thrown with the RIGHT Hand, let the hand follow and release...
            if (isThrownRight == true)
            {
                //lerp down hand effector weight as we release
                if (character.solver.rightHandEffector.positionWeight > 0)
                {
                    character.solver.rightHandEffector.target = transform;
                    character.solver.rightHandEffector.positionWeight = Mathf.Lerp(1f, 0, t);
                    //Look where we are throwing
                    //headLook.solver.target = target;
                    //headLook.solver.IKPositionWeight = Mathf.Lerp(0, 1.0f, t);
                    t += releaseSpeed * Time.deltaTime;
                }

                //Once the hand has returned to it's natural position...
                if (character.solver.rightHandEffector.positionWeight == 0)
                {
                    //Restore settings and lerp the head back to it's original position
                    t = 0.0f;
                    character.solver.rightArmChain.pull = storeRightHandPullWeight;
                    character.solver.rightHandEffector.target = null;
                    isThrownRight = false;

                    //HeadLerpOut();    //<---- TODO Fix this
                }
            }

            //Debug with 'S'
            if (Input.GetKeyDown("s"))
            {
                //HeadLerpOut();

                //transform.parent = leftHand;
                //transform.position = new Vector3(0,0,0);
                // rb.isKinematic = true;
                // character.solver.leftHandEffector.positionWeight = 0f;
            }

        }
        else
        {
            target.gameObject.SetActive(false);
            target.transform.parent = character.transform;
            target.transform.rotation = new Quaternion(0, 0, 0, 0);

            target.transform.localPosition = new Vector3(0, 0.01f, 1);
        }

    }


    void ObjectForce()
    {

        var rigid = GetComponent<Rigidbody>();

        Vector3 p = target.position;

        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (p.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // Fire!
        //rigid.velocity = finalVelocity;

        // Alternative way:
        rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
    }


    void WindUp()
    {
       //print("fhdsjkfhdsjk");
        //Get start values
        bool once = false;
        if (once == false)
        {
            //character.solver.leftHandEffector.target = overhandThrowMarkerL;
            //character.solver.leftShoulderEffector.target = overhandShoulderMarkerL;

            startHandWeightL = character.solver.leftHandEffector.positionWeight;
             
            startHandRotWeightL = character.solver.leftHandEffector.rotationWeight;
            startRelHandPosWeightL = character.solver.leftHandEffector.maintainRelativePositionWeight;
            startShoulderWeightL = character.solver.leftShoulderEffector.positionWeight;
            startPullL = character.solver.leftArmChain.pull;
            startReachL = character.solver.leftArmChain.reach;
            pushParentL = character.solver.leftArmChain.pushParent;
            //bendGoalWeightL = character.solver.leftArmChain.pull;
            once = true;
        }



        //Throwing From The Left Hand
        if (/*transform.parent == leftHand && */throwOverhead == true)
        {

            print("t " + t + "weight" + character.solver.leftHandEffector.positionWeight);

            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > 1f)
            {
                currentLerpTime = 1f;
            }
            t = currentLerpTime / overhandlerpTime;

            // t = currentLerpTime / overhandlerpTime;
            // t = Mathf.Sin(t * Mathf.PI * 0.5f);

            //Get The targets

            //Lerp effectors to targets
            /*character.solver.leftHandEffector.positionWeight = t;//Mathf.Lerp(startHandWeightL, 1, t);
            character.solver.leftHandEffector.rotationWeight = Mathf.Lerp(startHandRotWeightL, 1, t);
            character.solver.leftHandEffector.maintainRelativePositionWeight = Mathf.Lerp(startRelHandPosWeightL, 1, t);
            character.solver.leftShoulderEffector.positionWeight = Mathf.Lerp(startShoulderWeightL, 1, t);
            character.solver.leftArmChain.pull= Mathf.Lerp(startPullL, 1, t);
            character.solver.leftArmChain.reach= Mathf.Lerp(startReachL, 1, t);
            character.solver.leftArmChain.pushParent = Mathf.Lerp(pushParentL, 1, t);*/

            character.solver.leftHandEffector.positionWeight = t;//Mathf.Lerp(startHandWeightL, 1, t);
            character.solver.leftHandEffector.rotationWeight = t;
            character.solver.leftHandEffector.maintainRelativePositionWeight = t;
            character.solver.leftShoulderEffector.positionWeight =  t;
            character.solver.leftArmChain.pull = t;
            character.solver.leftArmChain.reach = t;
            character.solver.leftArmChain.pushParent = t;

            //character.solver.leftArmChain.bendConstraint = Mathf.Lerp(startReachL, 1, t);


        }
    }
}

/*
//Reset the Head LookAt Position
void HeadLerpOut()
{

    //While we are still looking at the target...
    if (headLook.solver.IKPositionWeight > 0)
    {
        print(headLook.solver.IKPositionWeight);
        headLook.solver.IKPositionWeight = headLook.solver.IKPositionWeight - 0.001f;
        //Loop
        HeadLerpOut();

        //Alternatively (Preferably) Lerp It *** Don't know why this is buggin
        //Mathf.Lerp(headLook.solver.IKPositionWeight, 0, t);
        //t += headSpeed * Time.deltaTime;
    }
    else
    {
        headLook.solver.target = null;
    }
}*/