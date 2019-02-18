using UnityEngine;
using System.Collections;
using SnowDay.Diego.GameMode;
using SnowDay.Diego.CharacterController;
using System.Collections.Generic;

/// <summary>
/// Responsible for displaying controls and teams while 
/// </summary>
public class LoadingScreen : MonoBehaviour 
{
    [Tooltip("The parent gameobject of the all the loading screen elements")]
    [SerializeField] GameObject LoadingScreenParentGameobject = null;
    [Tooltip("The extra time added after displaying the loading screen, before entering gameplay")]
    [SerializeField] float LoadScreenDelay = 1.0f;

	void Awake() 
    {
        this.StartCoroutine(this.PregameLoadingScreenTimer());
    }

    /// <summary>
    /// Disables all player characters and gamemode manager, wait for some time then 
    /// destroy loading screen and reable those things.
    /// </summary>
    IEnumerator PregameLoadingScreenTimer() 
    {
        List<PlayerController> Players = GameModeController.GetInstance().GetActivePlayers();
        foreach(PlayerController Player in Players) 
        {
            Player.enabled = false;
        }

        yield return new WaitForSecondsRealtime(this.LoadScreenDelay);

        foreach(PlayerController Player in Players)
        {
            Player.enabled = true;
        }

        Destroy(this.LoadingScreenParentGameobject);
        GamemodeManagerBase.Instance.enabled = true;
    }
}
