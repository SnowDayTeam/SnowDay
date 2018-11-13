using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    public bool Pause;


    // Use this for initialization
    void Start () {
        Pause = false;

    }

    // Update is called once per frame
    void Update () {
        if (Pause)
            Time.timeScale = 0; 
        else 
            Time.timeScale = 1; 
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") 
        {
            Pause = true;
            print(col.gameObject.name); 
        }
    }


}
