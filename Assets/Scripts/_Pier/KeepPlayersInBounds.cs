using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnowDay.Diego.CharacterController;

public class KeepPlayersInBounds : MonoBehaviour
{
    GamemodeManagerBase modeManager;
    Collider myCollider;
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
    // Use this for initialization
    void Start ()
    {
        modeManager = FindObjectOfType<GamemodeManagerBase>();
        myCollider = gameObject.AddComponent<BoxCollider>();
        gameObject.layer = 2;
    }
	
	// Update is called once per frame
	void Update ()
    {
       

        foreach ( PlayerController p in modeManager.Players)
        {

           if(myCollider.bounds.Contains(p.GetCharacterPosition()) == false)
           {
                p.MoveCharacter(transform.position);
           }
        }

    }
}
