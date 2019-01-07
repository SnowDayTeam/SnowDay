using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    float currentDelay = 5;
    bool countDown = false;

    void Update()
    {
        if(countDown)
        currentDelay -= Time.deltaTime;


        if (currentDelay <= 0)
            StartCoroutine(LoadAsynchronously(5));
    }

	public void LoadLevel(int sceneIndex)
    {
        countDown = true;
        loadingScreen.SetActive(true);
        

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
          
            yield return null;
        }


    }

 
}
