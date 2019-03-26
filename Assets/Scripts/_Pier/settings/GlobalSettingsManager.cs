using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettingsManager : MonoBehaviour
{
    public static GlobalSettingsManager _Instance;
    public static GlobalSettings s;
    public  GlobalSettings MainSettings;
    // Use this for initialization
    void Awake ()
    {
        DontDestroyOnLoad(gameObject);
        if (GlobalSettingsManager._Instance == null)
        {
            GlobalSettingsManager._Instance = this;
        }
        else
        {
            Debug.LogWarning("A previously awakened Settings MonoBehaviour exists!", gameObject);
            Destroy(this.gameObject);
        }
        if (GlobalSettingsManager.s == null)
        {
            GlobalSettingsManager.s = MainSettings;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
