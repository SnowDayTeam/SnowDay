using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_Manager : MonoBehaviour {

    [SerializeField] GameObject MainPanel;    
    [SerializeField] GameObject GamePanel;

    // Use this for initialization
    void Start ()
    {
        MainPanel.gameObject.SetActive(true);
        GamePanel.gameObject.SetActive(false);
    }
	
    public void modeOne()
    {
        SceneManager.LoadScene("Snoccer");
    }

    public void modeTwo()
    {
        SceneManager.LoadScene("Level");
    }

    public void modeThree()
    {
        SceneManager.LoadScene("Level");
    }

    public void modeFour()
    {
        SceneManager.LoadScene("Level");
    }

    public void controls()
    {
        MainPanel.gameObject.SetActive(false);
        GamePanel.gameObject.SetActive(true);
    }

    public void back()
    {
        MainPanel.gameObject.SetActive(true);
        GamePanel.gameObject.SetActive(false);
    }

    public void Ending()
    {
        SceneManager.LoadScene("Menu");
    }
}
