using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelTest : MonoBehaviour
{
    Material myMaterial;
    Texture2D test;
    public TeamManager teamManager;
    // Use this for initialization
    void Start() {
        myMaterial = GetComponent<MeshRenderer>().material;
        myMaterial.mainTexture = teamManager.splatmap; //("_Albedo", SnowTackScript.splatmap);

    }

    // Update is called once per frame
    void Update() {

    }
    public void getPixel()
    {

    }
}
