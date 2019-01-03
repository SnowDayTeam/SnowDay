using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {

    Texture2D MapChecker;
    int RedPixelCounter;
    private float dirtyTimer = 0;
    public float dirtyTimeSkip = .2f;
    

	// Use this for initialization
	void Start () {
		MapChecker = new Texture2D(1024, 1024, TextureFormat.RGBAFloat, false);
    }
	
	// Update is called once per frame
	void Update () {
        dirtyTimer += Time.deltaTime;
        if (dirtyTimer >= dirtyTimeSkip)
        {
            RenderTexture.active = SnowTackScript.splatmap;
            MapChecker.ReadPixels(new Rect(0, 0, SnowTackScript.splatmap.width, SnowTackScript.splatmap.height), 0, 0);
            MapChecker.Apply();
            dirtyTimer = 0;

        }

        CheckScore();

      
    }

    

    public void CheckScore()
    {
        for (int x = 0; x < SnowTackScript.splatmap.width; x++)
        {
            for (int y = 0; y < SnowTackScript.splatmap.height; y++)
            {
                Color temp2 = MapChecker.GetPixel(x, y);
                Debug.Log(temp2);
                if (temp2.r > .9)
                {
                    RedPixelCounter++;
                    Debug.Log(RedPixelCounter);
                }
            }
        }
    }
}
