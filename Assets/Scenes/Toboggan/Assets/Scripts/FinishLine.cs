using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    //bool to check if game is paused
    public bool Pause;


    // Use this for initialization
    void Start ()
    {
        //start off game as running (not-paused)
        Pause = false;
    }

    // Update is called once per frame
    void Update ()
    {
        //pause game if pause bool is set to true
        if (Pause)
            Time.timeScale = 0; 
        else 
            Time.timeScale = 1; 
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Player") || col.gameObject.name.Contains("Sled")) 
        {
            Pause = true; 
            print(col.gameObject.name);

            //game restart
            Application.LoadLevel(Application.loadedLevel);
        }
    }


}
