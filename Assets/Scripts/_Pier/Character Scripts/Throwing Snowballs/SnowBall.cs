using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {

    Rigidbody rigid;
    private bool Grow;
    public float moveTime;
    Vector3 localSize;
    Vector3 angleRot;
	public float test = 5;
	public float test2 = 9;
	public float test3 = 13;

	//public com.ootii.Actors.AnimationControllers.MotionController mController;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        //moveTime = 0.0002f;
        localSize = gameObject.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
	//	print(mController.Animator.name);
        gameObject.transform.localScale = localSize;

        float xVel = rigid.angularVelocity.x;
        float yVel = rigid.angularVelocity.y;
        float zVel = rigid.angularVelocity.z;

        float avg = (Mathf.Abs(xVel) + Mathf.Abs(yVel) + Mathf.Abs(zVel)) / 3;
        angleRot = new Vector3();


        if (Grow && avg >= 0.1f && rigid.velocity.magnitude > 0)
        {
            localSize += new Vector3(avg, avg, avg) * moveTime;
            rigid.mass += avg * moveTime * 4;
            rigid.angularDrag += avg * moveTime;
            //rigid.drag += (avg * moveTime) * 0.75f;

            //localSize.x += /*rigid.velocity.x + */ rigid.velocity.z * moveTime;
            //localSize.y += /*rigid.velocity.x +*/ rigid.velocity.z * moveTime;
            //localSize.z += /*rigid.velocity.x + */rigid.velocity.z * moveTime;
            //localSize.x += rigid.angularVelocity.x * moveTime;// + Time.deltaTime * moveTime;
            // localSize.y += rigid.angularVelocity.x + Time.deltaTime * moveTime;
            // localSize.z += rigid.angularVelocity.x + Time.deltaTime * moveTime;
        }
        
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Snow")
            Grow = true;
        else
            Grow = false;
    }
}
