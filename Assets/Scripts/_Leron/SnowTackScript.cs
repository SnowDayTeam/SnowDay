﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTackScript : MonoBehaviour
{
    public SnowPlane mySnowPlane;
    //public static RenderTexture splatmap;// made this static so refference is shared between all instances  // used by ShovelProjectile keep it public 
    //public Shader drawShader;

    //public Transform[] _feet;
    public SnowTrackData[] snowTracks;
    RaycastHit _groundHit;
    public LayerMask _layerMask;
    //[Range(0,2)]
   // public float _brushSize;
   // [Range(0, 1)]
   // public float _brushStrength;

    public void toggleSnowTrack(int index, bool val)
    {
        if(index < snowTracks.Length)
        {
            snowTracks[index].active = val;
        }
    }
    // Use this for initialization
    void Awake () {
    //    _layerMask = LayerMask.GetMask("Ground");
       
	}
	
	// Update is called once per frame
	void Update () {
        //for(int i = 0; i < _feet.Length; i++)
        //      {
        //          if (Physics.Raycast(_feet[i].position, -Vector3.up, out _groundHit, 1f, _layerMask))
        //          {
        //              drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
        //              drawMaterial.SetFloat("_Strength", _brushStrength);
        //              drawMaterial.SetFloat("_Size", _brushSize);
        //              RenderTexture temp = RenderTexture.GetTemporary(splatmap.width, splatmap.height, 0, RenderTextureFormat.ARGBFloat);
        //              Graphics.Blit(splatmap, temp);
        //              Graphics.Blit(temp, splatmap, drawMaterial);
        //              RenderTexture.ReleaseTemporary(temp);
        //          }
        //      }

        
        for (int i = 0; i < snowTracks.Length; i++)
        {

            if (snowTracks[i].active && snowTracks[i].transform != null && Physics.Raycast(snowTracks[i].transform.position, -Vector3.up, out _groundHit, 1f, _layerMask))
            {
                //  Debug.Log(_groundHit.textureCoord.x.ToString() + "," + _groundHit.textureCoord.y.ToString());

                mySnowPlane.drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
                mySnowPlane.drawMaterial.SetFloat("_Strength", snowTracks[i]._brushStrength);
                mySnowPlane.drawMaterial.SetFloat("_Size", snowTracks[i]._brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(mySnowPlane.splatmap.width, mySnowPlane.splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(mySnowPlane.splatmap, temp);
                Graphics.Blit(temp, mySnowPlane.splatmap, mySnowPlane.drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }
}
