using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.Input;

[RequireComponent (typeof(InputManager))]
public class LevelSelectPlayerActivator : MonoBehaviour {

    public PierInputManager.ButtonName ActivationButton = PierInputManager.ButtonName.A;

    public Transform[] players = new Transform[16];

    private InputManager inputController;


	// Use this for initialization
	void Start () {
        inputController = GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < players.Length; i++)
        {
           bool keyPressed = inputController.GetButton((PlayerNumber)i, (ButtonName)ActivationButton);
           if (keyPressed)
           {


           }
        }
		
	}
}
