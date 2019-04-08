using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//***** This script requires a VotingDisplayCanvas prefab as a child of the portal this script is attached to *****//

public class LevelSelect_Voting : MonoBehaviour {

    private int voteCount = 0;
    private Canvas voteCanvas;
    Transform particles;
    float OGtime;
    public float timer = 3.0f;

    private void Start()
    {
        particles = transform.GetChild(0);
        //particles = GetComponentInChildren<ParticleSystem>().transform;
        OGtime = timer;
    }

    private void Update()
    {
        if (particles.gameObject.activeSelf == true)
        {
            Timer();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Animator>()) {
            voteCanvas = GetComponentInChildren<Canvas>(true).rootCanvas;

            //if the canvas is inactive, turn it on
            if (!voteCanvas.enabled) {
                voteCanvas.enabled = true;
                if (particles.gameObject.activeSelf == false)
                {
                    particles.gameObject.SetActive(true);
                    
                }
            }

            //increase number of votes and display result to canvas, change line 25 if using different text/display method
            voteCount++;
            voteCanvas.GetComponentInChildren<Text>().text = voteCount.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if player exits vote area, decrease vote count and output result to canvas, change line 34 if using different text/display method
        if (other.gameObject.GetComponent<Animator>()) {
            voteCount--;
            voteCanvas.GetComponentInChildren<Text>().text = voteCount.ToString();

            //if there are no players left voting, disable the canvas
            if (voteCount <= 0)
            {
                voteCanvas.enabled = false;
            }

        }
    }
    void Timer()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            particles.gameObject.SetActive(false);
            timer = OGtime;
        }
    }
}
