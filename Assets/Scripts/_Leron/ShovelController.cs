using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using RootMotion.FinalIK;

public class ShovelController : MonoBehaviour
{
    public SnowTackScript tackScript;
    public Rigidbody PlayerRB;
    public bool isInSnowArea = false;
    public float currentsSnowVolume = 0;
    public float maxSnowVolume = 100.0f;
    public float snowWeight = 3.0f;
    public float snowAcumulationRate = 0.5f;
    public FullBodyBipedIK IK;

    public Text VolumeText;
    public Text WeightText;
    SnowSize snowScript;

    //Hunters Stuff
    public Transform SpawnPoint;
    public float LaunchVelocity=5;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "SnowArea")
        {
            isInSnowArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        isInSnowArea = false;

    }

    // Use this for initialization
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
        snowScript = GetComponentInChildren<SnowSize>();
        
        //IK.solver.leftHandEffector.positionWeight = 1;
    }

    // Update is called once per frame
    void Update()
    {

        /* 
         
       -----------------------------------------------------------------      SCORE UPDATE      ------------------------------------------------------------------------------  
         if (Input.GetKeyDown("space"))
          {
              GameObject eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
              ScoreManager eventSystemScript = eventSystem.GetComponent<ScoreManager>();
              eventSystemScript.ScoreUpdate(2);

          }
       ------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        */

        if (CrossPlatformInputManager.GetButtonDown("FireP1") && isInSnowArea)
        {
            tackScript.toggleSnowTrack(2, true);
        }

        if (CrossPlatformInputManager.GetButtonUp("FireP1"))
        {
            tackScript.toggleSnowTrack(2, false);
        }

        if (CrossPlatformInputManager.GetButton("FireP1") && isInSnowArea && PlayerRB.velocity.magnitude > 3 && currentsSnowVolume != maxSnowVolume)
        {

            currentsSnowVolume += snowAcumulationRate;
            VolumeText.text = "Snow Amount " + currentsSnowVolume.ToString("F1");
            // GameObject snow = GameObject.FindGameObjectWithTag("Snow");
              
            snowScript.setSnowPercent(currentsSnowVolume / maxSnowVolume);            
        }

        if (currentsSnowVolume >= maxSnowVolume || isInSnowArea == false)
        {
            tackScript.toggleSnowTrack(2, false);

        }

        if (CrossPlatformInputManager.GetButton("FireP1") && isInSnowArea)
        {
            //PlayerRB.drag = (currentsSnowVolume / snowWeight);
            if (currentsSnowVolume >= maxSnowVolume)
            {
                GetComponent<MoveModifier>().ModifyMovement(MoveModifier.MoveModes.Stop , currentsSnowVolume / maxSnowVolume);
            }
            else
            {
                GetComponent<MoveModifier>().ModifyMovement(MoveModifier.MoveModes.Slow, currentsSnowVolume / maxSnowVolume);
            }
               

                WeightText.text = "Snow Weight " + PlayerRB.drag.ToString("F1") + " lbs";
        }

        if (CrossPlatformInputManager.GetButtonUp("FireP1"))
        {

            GetComponent<MoveModifier>().ModifyMovement(MoveModifier.MoveModes.Reset,currentsSnowVolume/maxSnowVolume);
        }

        //if (CrossPlatformInputManager.GetButton("JumpP1"))
        //{
        //    //tackScript.toggleSnowTrack(2, false);
        //    Dropsnow();
        //}
        ThrowShovelSnow();
            


    }



    void Dropsnow()
    {
        currentsSnowVolume = 0.0f;
        PlayerRB.drag = 0.0f;
        WeightText.text = "Snow Weight " + PlayerRB.drag.ToString("F1") + " lbs";
        VolumeText.text = "Snow Amount " + currentsSnowVolume.ToString("F1");

        //GameObject snow = GameObject.FindGameObjectWithTag("Snow");
      
        snowScript.setSnowPercent( 0.0f);

    }

    void ThrowShovelSnow()
    {
        if (CrossPlatformInputManager.GetButtonDown("JumpP1")&& currentsSnowVolume>10.0)
        {
            GameObject ShovelPro = Instantiate(Resources.Load("PRE_ShovelProjectile")) as GameObject;
            ShovelPro.GetComponent<SnowSize>().setSnowPercent(currentsSnowVolume / maxSnowVolume);
            ShovelPro.GetComponent<ShovelProjectile>().SetBrushSize(currentsSnowVolume / maxSnowVolume);
            ShovelPro.transform.position = SpawnPoint.position;
            ShovelPro.GetComponent<Rigidbody>().velocity = transform.forward * LaunchVelocity;
            

          //Set object variables to snow weight as well as it's scale here


            Dropsnow();
           
            
        }

    }

}
    
