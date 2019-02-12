using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreIncrease : MonoBehaviour {

    //cylinder above score cards, which they rotate around
    [SerializeField] GameObject holder;
    
    //new scorecard is used for new numbers rotating on
    [SerializeField] GameObject newScoreCard;
    [SerializeField] TextMeshPro newScoreText;
    [SerializeField] GameObject newScoreCardTen;
    [SerializeField] TextMeshPro newScoreTextTen;

    //old scorecard is used for number currently displayed on score card
    [SerializeField] GameObject oldScoreCard;
    [SerializeField] TextMeshPro oldScoreText;
    [SerializeField] GameObject oldScoreCardTen;
    [SerializeField] TextMeshPro oldScoreTextTen;

    bool rotation;

    //current score
    int currentScore = 0;
    
    Quaternion rot; // = transform.eulerAngles;

    // Use this for initialization
    void Start () {
        //start position of both new score cards should be behind board
        newScoreCard.transform.RotateAround(holder.transform.position, new Vector3(0, 0, -1), 20);
        newScoreCardTen.transform.RotateAround(holder.transform.position, new Vector3(0, 0, -1), 20);

        //InvokeRepeating("plusOne", 3.0f, 2.0f); 
        //testing
        setScore(6);

    }
	
	void Update () {

        if (rotation)
        {
            newScoreCard.transform.RotateAround(holder.transform.position, new Vector3(0, 0, -1), 200 * Time.deltaTime); 
        }
        if (((currentScore % 10 == 0) ||(oldScoreTextTen.text.ToString() != (currentScore/10).ToString())) && rotation)
        {
            newScoreCardTen.transform.RotateAround(holder.transform.position, new Vector3(0, 0, -1), 225 * Time.deltaTime); 
        }

        //once card near resting position, stop rotation, and use 'old score card' as display, moving 'new score card' behind board again for next new #
        if (newScoreCard.transform.rotation.z < 15 && newScoreCard.transform.rotation.z > 0)
        {
            rotation = false;
            oldScoreText.text = (currentScore%10).ToString();
            newScoreCard.transform.RotateAround(holder.transform.position, new Vector3(0, 0, 1), 320); 
        }
        //same as above, but for 10's digits
        if (newScoreCardTen.transform.rotation.z < 15 && newScoreCardTen.transform.rotation.z > 0)
        {   
            newScoreCardTen.transform.RotateAround(holder.transform.position, new Vector3(0, 0, 1), 355);
            oldScoreTextTen.text = (currentScore / 10).ToString(); 
        }


    }
    
    public void setScore(int _score)
    {
        currentScore += _score; 
        rotation = true; 
         newScoreText.text = (currentScore%10).ToString();
        newScoreTextTen.text = (currentScore / 10).ToString();
    }
}
