using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.CharacterController;
using SnowDay.Diego.GameMode;
public class ShovelWarManager : MonoBehaviour
{
    List<PlayerController> AllPlayers;
    public Transform ScriptsPrefab;

    // Use this for initialization
    void Start ()
    {
        AllPlayers = GameModeController.GetInstance().GetActivePlayers();
        for (int i = 0; i < AllPlayers.Count; i++)
        {
            Debug.Log(AllPlayers[i].gameObject.name);
            Instantiate(ScriptsPrefab, AllPlayers[i].gameObject.transform.GetChild(0).GetChild(2), false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
