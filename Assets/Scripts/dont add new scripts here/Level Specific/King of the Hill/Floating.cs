using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

    public bool _moving = true;

    private bool moveLeft = true;

	//private bool downwards;
    private float _direction;

    private float _elapsedTime;

	public GameObject snowBall;

	public Transform spawner;

	public float dirSwitchTime = 4;

    public float Speed;

    //alternateMovement
    public float MaxX;
    public float MinX;



	void Update ()
    {
		//set timer
        _elapsedTime += Time.deltaTime;


        //Move object back and forth
        if (_moving)
        {
            //Debug.Log(_elapsedTime);
			if (transform.position.x >= MaxX && moveLeft)
            {
                //_elapsedTime = 0;
                Debug.Log("left limit");
                
				moveLeft = false;
            }
			else if (transform.position.x <= MinX && !moveLeft)
            {
                //_elapsedTime = 0;
				Debug.Log("right limit");

                moveLeft = true;
            }

            if (moveLeft)
            {
                transform.Translate(Speed, 0, 0);
            }
            else if (!moveLeft)
            {
                transform.Translate(-Speed, 0, 0);
            }
            
        }

        //Instantiate at timed intervals
        if (_elapsedTime == 1 || _elapsedTime == 2 || _elapsedTime == 3 || _elapsedTime == 4)
        {
			Instantiate(snowBall, spawner);
        }

        
	}
}
