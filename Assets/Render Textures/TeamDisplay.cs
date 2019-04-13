using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamDisplay : MonoBehaviour {
    public Texture[] characterImages;
    public GameObject[] teamPanels;
    public GameObject ImagePrefab;
    public GameObject KidsPrefabs;
    public GameObject Test;
    // Use this for initialization
    [ContextMenu ("fuck this shit")]
    public void Setup () {

        Test = Instantiate(KidsPrefabs);
       // Debug.Log(SceneManager.GetActiveScene().name);
      //  Debug.Break();
        Debug.Log("-------------------------");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
