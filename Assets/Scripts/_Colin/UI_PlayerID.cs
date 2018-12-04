using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerID : MonoBehaviour {

    public GameObject player;
    private Vector3 startSize;
    public Image[] teamBackgrounds;
    [Range(2.5f, 6)] public float yOffset;

    private void Awake(){
        yOffset = 2.5f;
        //temp line, will need to user inputManager.getplayerID
        int playerNum = FindObjectOfType<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().playerNumber;
        startSize = transform.localScale;
        //setup player ID text/background based on player ID number and team color, will need to get/find playerID so this can be used in each scene
        Text textField = GetComponentInChildren<Text>();
        textField.text = "P" + playerNum;
       // Image idBackground = GetComponentInChildren<Image>();
        //maybe set background image to match team color/logo?
        //idBackground = teamBackgroundImage[teamNumber];
       

    }

    public IEnumerator ExpandIDCanvas(float duration) {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration) {
            Vector3 vecScale = new Vector3(Mathf.Sin(Time.time * 2) + 1, Mathf.Sin(Time.time * 2) + 1, 0);
            transform.localScale = vecScale * 0.5f;

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = startSize;
    }

    
    void Update () {
        //set look at position relative to camera orientation, set position to player, will need to update to get playerID, then playerID.transform
        transform.LookAt( 2 * transform.position - Camera.main.transform.position);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z);
        // need to get input based on playerID, then run expand coroutine for that player.
        if (Input.GetKeyDown(KeyCode.Q)) {
            StartCoroutine(ExpandIDCanvas(2.0f));
        }
       
	}
}
