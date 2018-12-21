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
    public Transform ShovelPoint;// need to change to a more flexible way of getting shovel point

    //temporary variables to stand in until we can read pixels from splat map
    private Vector3 LastPosition;
    public float DistanceRequired = .04f;

    //snow Shader stuff
    public LayerMask Mask;
    public Shader drawShader;
    static private Material drawMaterial;
    


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
        drawMaterial = new Material(drawShader);
        
        
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
            LastPosition = ShovelPoint.transform.position;
        }

        if (CrossPlatformInputManager.GetButtonUp("FireP1"))
        {
            tackScript.toggleSnowTrack(2, false);
            GetComponent<MoveModifier>().ModifyMovement(MoveModifier.MoveModes.Reset, currentsSnowVolume / maxSnowVolume);
        }

        if (CrossPlatformInputManager.GetButton("FireP1") && isInSnowArea && currentsSnowVolume != maxSnowVolume)
        {
            TEMPSnowAccumulation();
            //currentsSnowVolume += snowAcumulationRate;
            VolumeText.text = "Snow Amount " + currentsSnowVolume.ToString("F1");
            // GameObject snow = GameObject.FindGameObjectWithTag("Snow");

            //check if ground has snow stop checking magnitude above.
           
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

    

        //if (CrossPlatformInputManager.GetButton("JumpP1"))
        //{
        //    //tackScript.toggleSnowTrack(2, false);
        //    Dropsnow();
        //}
        ThrowShovelSnow();
            


    }

    void CheckIfSnowShoveled()
    {
        //RaycastHit hit = new RaycastHit();

        //Ray ray = new Ray(ShovelPoint.position, -Vector3.up);
        //Debug.DrawRay(ShovelPoint.position, -Vector3.up, Color.cyan, 10);
        //if (Physics.Raycast(ray, out hit, 500, Mask))
        //{
        //    //Debug.Log(hit.collider.gameObject.name);
        //    Material hitMat = hit.transform.GetComponent<Material>();
        //    int textY= hit.textureCoord.y ;
        //    int textX =hit.textureCoord.x;
        //    Texture2D Checker = new Texture2D(1024,1024,TextureFormat.RGBAFloat,false);

            
            

           

        //    Debug.Log(hit.textureCoord.y);
            //hit.textureCoord



            //Material myMaterial;
            //myMaterial = hit.collider.gameObject.GetComponent<MeshRenderer>().material;

            //drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            //RenderTexture temp = RenderTexture.GetTemporary(SnowTackScript.splatmap.width, SnowTackScript.splatmap.height, 0, RenderTextureFormat.ARGBFloat);   //TO CHANGE LATER 
            //Graphics.Blit(SnowTackScript.splatmap, temp);
            //Graphics.Blit(temp, SnowTackScript.splatmap, drawMaterial);
            //RenderTexture.ReleaseTemporary(temp);
        


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
    
