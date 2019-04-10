using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelProjectile : MonoBehaviour
{

    public LayerMask Mask;
    public Shader drawShader;
    static private Material drawMaterial;
    [Range(0, 12)]
    public float _brushSize;
    public float brushSizeMaximum=8;
    [Range(-1, 1)]
    public float _brushStrength = -1;
    public int TimesToCheck = 4;
    float spawnTime;
    float LifeTime=3f;


    // Use this for initialization
    void Start () {
        drawMaterial = new Material(drawShader);
        spawnTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time - spawnTime > LifeTime)
        {
            Destroy(gameObject);
        }
		
	}
    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        //if(collision.collider.tag == "Player")
        //{
        //    Destroy(gameObject);
        //}

        //else { }
  
    

        for (int i = 0; i < TimesToCheck; i++)
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(collision.contacts[0].point + collision.contacts[0].normal, -collision.contacts[0].normal * 2);
            Debug.DrawRay(ray.origin, ray.direction, Color.cyan, 10);
            if (Physics.Raycast(ray, out hit, 500, Mask))
            {
                SnowPlane manager = hit.collider.gameObject.GetComponent<SnowPlane>();
                GetComponent<AudioSource>().Play();
                if(manager != null)
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    Material myMaterial;
                    myMaterial = hit.collider.gameObject.GetComponent<MeshRenderer>().material;

                    drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
                    drawMaterial.SetFloat("_Strength", _brushStrength);
                    drawMaterial.SetFloat("_Size", _brushSize);
                    RenderTexture temp = RenderTexture.GetTemporary(manager.splatmap.width, manager.splatmap.height, 0, RenderTextureFormat.ARGBFloat);   //TO CHANGE LATER 
                    Graphics.Blit(manager.splatmap, temp);
                    Graphics.Blit(temp, manager.splatmap, drawMaterial);
                    RenderTexture.ReleaseTemporary(temp);
                }
               
                if (i >= 3)
                {
                    Destroy(gameObject);
                }


                
            }

            
            
        }

      

    }


    public void SetBrushSize(float percentage)
    {
        _brushSize = percentage * brushSizeMaximum*4;
    }

}
