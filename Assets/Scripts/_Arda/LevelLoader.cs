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
        

       
    }

	public void LoadLevel(int sceneIndex)
    {
        countDown = true;
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(5));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        operation.allowSceneActivation = false;
        while(!operation.isDone)
        {

            Debug.Log(operation.progress);
            if (currentDelay >= maxDelay)
            {
                Debug.Log(currentDelay);
                operation.allowSceneActivation = true;


            }
            yield return null;
        }

        

    }

 
}
