using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    float currentDelay;
    [SerializeField]

    float maxDelay = 5;
    bool countDown = false;
    private void Start()
    {
        currentDelay = 0;
    }
    void Update()
    {
        if (countDown)
        {
            currentDelay += Time.deltaTime;

            float progress = Mathf.Clamp01(currentDelay / maxDelay);

            slider.value = progress;
        }
        

        if (currentDelay >= maxDelay)
        {
            StartCoroutine(LoadAsynchronously(5));

        }
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
          
          
            yield return null;
        }


    }

 
}
