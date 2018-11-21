using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Detention_TimerScript : MonoBehaviour {

    [SerializeField] private Text timerText;
    [Range(10, 180)]
    [SerializeField] public float timer;

	// Use this for initialization
	void Start () {
        timer = 180;
        timerText = gameObject.GetComponent<Text>();
        timerText.text = "Timer: " + timer;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        timerText.text = "Timer: " + timer.ToString("f0");
        if (timer <= 0) {
            timer = 0;
            timerText.color = Color.blue;
            timerText.text = "Kids Win!";
        }
	}

    public void SetTimerText(string s, Color c) {
        timerText.text = s;
        timerText.color = c;
        Debug.Log(timerText.text);
    }
}
