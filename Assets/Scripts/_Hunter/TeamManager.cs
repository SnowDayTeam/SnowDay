using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [HideInInspector]
    public Texture2D MapChecker;
    [SerializeField]
    public float RedPixelCounter;
    private float dirtyTimer = 0;
    public float dirtyTimeSkip = .2f;
    Color[] pixels;
    [HideInInspector]
    public Material drawMaterial;
    [HideInInspector]
    public Material myMaterial;
    [HideInInspector]
    public RenderTexture splatmap;// made this static so refference is shared between all instances  // used by ShovelProjectile keep it public 
    public Shader drawShader;
    [HideInInspector]
    public GameObject _SnowPlane;

    // Use this for initialization
    private void Awake()
    {
        _SnowPlane = this.gameObject;
        drawMaterial = new Material(drawShader);
        myMaterial = _SnowPlane.GetComponent<MeshRenderer>().material;
        myMaterial.SetTexture("_Splat", splatmap = new RenderTexture(256, 256, 0, RenderTextureFormat.ARGBFloat));
    }
    void Start ()
    {
		MapChecker = new Texture2D(splatmap.width, splatmap.height, TextureFormat.RGBAFloat, false);

       
    }
	
	// Update is called once per frame
	void Update ()
    {
        dirtyTimer += Time.deltaTime;
        if (dirtyTimer >= dirtyTimeSkip)
        {
            RenderTexture.active = splatmap;
            MapChecker.ReadPixels(new Rect(0, 0, splatmap.width, splatmap.height), 0, 0);
            MapChecker.Apply();
            dirtyTimer = 0;
            pixels = MapChecker.GetPixels(0,0,splatmap.width, splatmap.height);
        }
        
        if(pixels!= null)
        {
            CheckScore();

        }

    }

    

    public void CheckScore()
    {
        RedPixelCounter = 0;
        for (int i = 0; i < pixels.Length; i++)
        {
            Color temp2 = pixels[i];
                
            if (temp2.r > .9)
            {
                RedPixelCounter++;
              
            }
            
        }
       // Debug.Log(RedPixelCounter);
    }
}
