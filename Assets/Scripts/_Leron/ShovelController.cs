using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using RootMotion.FinalIK;

public class ShovelController : MonoBehaviour
{
    public SnowTackScript tackScript;
    Rigidbody PlayerRB;
    public bool isInSnowArea = false;
    public float currentsSnowVolume = 0;
    public float maxSnowVolume = 100.0f;
    public float snowWeight = 3.0f;
    public float snowAcumulationRate = 0.5f;
  //  public FullBodyBipedIK IK;

  //  public Text VolumeText;
   // public Text WeightText;
    SnowSize snowScript;

    //Hunters Stuff
    public Transform SpawnPoint;
    public float LaunchVelocity=5;
    public Transform ShovelPoint;// need to change to a more flexible way of getting shovel point

    //temporary variables to stand in until we can read pixels from splat map
    private Vector3 LastPosition;
    public float DistanceRequired = .04f;

    //snow Shader stuff
    public LayerMask Mask;
    public Shader drawShader;
    static private Material drawMaterial;
    private float dirtyTimer = 0;
    public float dirtyFrameSkip = 0.1f;
    static Texture2D Checker;

    //temporary
    //public GameObject TM;
    public PierInputManager.ButtonName ShovelButton = PierInputManager.ButtonName.B;
    public PierInputManager.ButtonName ThrowButton = PierInputManager.ButtonName.Y;

    private PierInputManager playerInputController;
    private ShovelLerpController shovelLerp;
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
        playerInputController = gameObject.GetComponentInParent<PierInputManager>();
        shovelLerp = GetComponentInChildren<ShovelLerpController>();
        PlayerRB = GetComponent<Rigidbody>();
        snowScript = GetComponentInChildren<SnowSize>();
        drawMaterial = new Material(drawShader);
        //Checker = new Texture2D(1024, 1024, TextureFormat.RGBAFloat, false);
        
  

        //IK.solver.leftHandEffector.positionWeight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //TO BE REMOVED !!!
        //if (verydirtyBool)
        //{
        //    dirtyTimer +=Time.deltaTime;
        //    if( dirtyTimer >= dirtyFrameSkip)
        //    {
        //        RenderTexture.active = tackScript.myTeam.splatmap;
        //        Checker.ReadPixels(new Rect(0, 0, tackScript.myTeam.splatmap.width, tackScript.myTeam.splatmap.height), 0, 0);
        //        Checker.Apply();
        //        dirtyTimer = 0;
        //    }

        //}

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
        
        if (playerInputController.GetButtonDown(ShovelButton))
        {
            tackScript.toggleSnowTrack(2, true);
            LastPosition = ShovelPoint.transform.position;
            Debug.Log("Shoveling mode");
            if (shovelLerp)
            {
                shovelLerp.shovelling = true;
                shovelLerp.throwing = false ;

            }
        }

        if (playerInputController.GetButtonUp(ShovelButton))
        {
            tackScript.toggleSnowTrack(2, false);
            GetComponent<MoveModifier>().ModifyMovement(MoveModifier.MoveModes.Reset, currentsSnowVolume / maxSnowVolume);
            if (shovelLerp)
            {
                shovelLerp.shovelling = false;
            }
        }

        if (playerInputController.GetButton(ShovelButton) && currentsSnowVolume != maxSnowVolume)
        {
            //TEMPSnowAccumulation();
            //currentsSnowVolume += snowAcumulationRate;
            CheckIfSnowShoveled();
          //  Debug.Log(tackScript.snowTracks[2].active);
            //VolumeText.text = "Snow Amount " + currentsSnowVolume.ToString("F1");
            // GameObject snow = GameObject.FindGameObjectWithTag("Snow");

            //check if ground has snow stop checking magnitude above.
           
            snowScript.setSnowPercent(currentsSnowVolume / maxSnowVolume);            
        }

        if (currentsSnowVolume >= maxSnowVolume )
        {
            tackScript.toggleSnowTrack(2, false);

        }

        if (playerInputController.GetButton(ShovelButton))
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
               

                //WeightText.text = "Snow Weight " + PlayerRB.drag.ToString("F1") + " lbs";
        }

    

        //if (CrossPlatformInputManager.GetButton("JumpP1"))
        //{
        //    //tackScript.toggleSnowTrack(2, false);
        //    Dropsnow();
        //}
        ThrowShovelSnow();
            


    }

    void CheckIfSnowShoveled()
    {

       
        RaycastHit hit;

        Ray ray = new Ray(ShovelPoint.position, -Vector3.up);
       
        if (Physics.Raycast(ray, out hit, 500, Mask))
        {
           // Debug.DrawRay(ShovelPoint.position, -Vector3.up, Color.red, 10);
            //Debug.Log(hit.collider.gameObject.name);
            Material hitMat = hit.transform.GetComponent<Material>();
            SnowPlane plane = hit.transform.gameObject.GetComponent<SnowPlane>();
            tackScript.mySnowPlane = plane;

            int textX = (int)(tackScript.mySnowPlane.splatmap.width * hit.textureCoord.x);
            int textY = (int)(tackScript.mySnowPlane.splatmap.height *  hit.textureCoord.y);

            // Checker.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
           
            //  SnowTackScript.splatmap.

            //Color temp = Checker.GetPixel(textX, textY);
            Color temp = tackScript.mySnowPlane.MapChecker.GetPixel(textX, textY);
            //Debug.Log(textX + " :  " + textY);
            //Debug.Log(temp);




            if (temp.r < 1)
            {
                
                currentsSnowVolume += snowAcumulationRate;

                Material myMaterial;
                myMaterial = hit.collider.gameObject.GetComponent<MeshRenderer>().material;

           
            }



        }


       



    }

    public void Dropsnow()
    {
        currentsSnowVolume = 0.0f;
       
        //WeightText.text = "Snow Weight " + PlayerRB.drag.ToString("F1") + " lbs";
        //VolumeText.text = "Snow Amount " + currentsSnowVolume.ToString("F1");

        //GameObject snow = GameObject.FindGameObjectWithTag("Snow");
      
        snowScript.setSnowPercent( 0.0f);

    }

    void ThrowShovelSnow()
    {
        if (playerInputController.GetButtonDown(ThrowButton) && currentsSnowVolume>10.0)
        {
            GameObject ShovelPro = Instantiate(Resources.Load("PRE_ShovelProjectile")) as GameObject;
            ShovelPro.GetComponent<SnowSize>().setSnowPercent(currentsSnowVolume / maxSnowVolume);
            ShovelPro.GetComponent<ShovelProjectile>().SetBrushSize(currentsSnowVolume / maxSnowVolume);
            ShovelPro.transform.position = SpawnPoint.position;
            ShovelPro.GetComponent<Rigidbody>().velocity = transform.forward * LaunchVelocity;


            //Set object variables to snow weight as well as it's scale here

            if (shovelLerp)
            {
                shovelLerp.throwing = true;
            }
            Dropsnow();
           
            
        }

    }


    void TEMPSnowAccumulation()
    {

        Vector3 MovementDifference = ShovelPoint.transform.position - LastPosition;

        if(MovementDifference.x>DistanceRequired 
          || MovementDifference.z > DistanceRequired)
        {
            currentsSnowVolume += snowAcumulationRate;
            LastPosition = ShovelPoint.transform.position;
        }
        
    }




}
    
