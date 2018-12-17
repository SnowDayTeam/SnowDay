using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KingOfTheHillUI : MonoBehaviour {
    public Text WinMessage;
    public GameObject WinUI;
    

	

    public void SetWinText(string Winner)
    {
        WinUI.SetActive(true);
        WinMessage.text = Winner;
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
