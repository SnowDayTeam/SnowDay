using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class Garage : MonoBehaviour {
    public bool IsTeam2;
    public Snoccer snoccer;
    public GameObject prefab;
    public GameObject ballDrop;
    public FullBodyBipedIK p1IK;
    public FullBodyBipedIK p2IK;
    public FullBodyBipedIK p3IK;
    public FullBodyBipedIK p4IK;

    public 




    // Use this for initialization
    void Start() {
		//p1IK.gameObject.GetComponent<ReachL>()
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump"))
        {
            ResetIK();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "SnowBall")
        {
            //col.transform.position+= new Vector3(0,)
            //destroys current snowball and spanws a new one

            //ResetIK();
            //Destroy(col.gameObject);
           
            SpawnNewBall();
            print(col.tag);
			col.GetComponent<moveToKillBall>().dead = true;
            //col.gameObject.transform.localScale = new Vector3(1,1,1);
            //col.gameObject.transform.position = ballDrop.transform.position;  
            //Debug.Log(snoccer.pos);

            //gives a point to team 2 if team 2 is true, gives a point to team one if false3
            if (IsTeam2)
            {
                snoccer.Team2_Points++;
            }
            else
            {
                snoccer.Team1_Points++;
            }
        }
    }

    public void SpawnNewBall()
    {
        Instantiate(prefab, ballDrop.transform.position, ballDrop.transform.rotation);
    }

    void ResetIK(){
        print("RESETIK");
        p1IK.solver.leftHandEffector.positionWeight = 0;
        p1IK.solver.leftHandEffector.target = null;
        p1IK.solver.rightHandEffector.positionWeight = 0;
        p1IK.solver.rightHandEffector.target = null;

        p2IK.solver.leftHandEffector.positionWeight = 0;
        p2IK.solver.leftHandEffector.target = null;
        p2IK.solver.rightHandEffector.positionWeight = 0;
        p2IK.solver.rightHandEffector.target = null;

        p3IK.solver.leftHandEffector.positionWeight = 0;
        p3IK.solver.leftHandEffector.target = null;
        p3IK.solver.rightHandEffector.positionWeight = 0;
        p3IK.solver.rightHandEffector.target = null;

        p4IK.solver.leftHandEffector.positionWeight = 0;
        p4IK.solver.leftHandEffector.target = null;
        p4IK.solver.rightHandEffector.positionWeight = 0;
        p4IK.solver.rightHandEffector.target = null;

       

    }
}
