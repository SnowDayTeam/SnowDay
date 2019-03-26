using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetUpShovelWar : MonoBehaviour {

    GrabPole grabPole;
	// Use this for initialization
	void Start () {
        grabPole = transform.parent.gameObject.AddComponent<GrabPole>();
        grabPole.isShovelWar = true;
        grabPole.nearPole = true;
        grabPole.grabPole = true;
        grabPole.pole = transform.Find("Shovel Container");
	}
}
