using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Timer")]
    public float LevelSelectCountDownTime;

    private float timeElapsed;

    [Header("UI Text")]
    public Text CountDownText;

    [Header("Portal Spawn Locations")]
    public List<LevelPortal> PortalSpawnPoints;

    [Header("Game Levels")]
    public List<LevelData> Levels;

    // Use this for initialization
    //TODO: Make Random level select
    private void Start()
    {
        timeElapsed = LevelSelectCountDownTime;
        for (var x = 0; x < PortalSpawnPoints.Capacity; x++)
        {
            PortalSpawnPoints[x].Initialize(Levels[x]);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        timeElapsed -= Time.deltaTime;
        CountDownText.text = timeElapsed.ToString(CultureInfo.InvariantCulture);
        if (timeElapsed <= 0)
        {
            LevelData selectedLevel = NextLevelSelect();
            if (selectedLevel)
            {
                SceneManager.LoadScene(selectedLevel.SceneName);
            }
        }
    }

    private LevelData NextLevelSelect()
    {
        LevelPortal currentHighestLevel = null;
        var highestPlayerCount = 0;

        for (int x = 0; x < PortalSpawnPoints.Count; x++)
        {
            Debug.Log(PortalSpawnPoints[x].name);
            var playerCount = PortalSpawnPoints[x].PlayersInBox;
            if (highestPlayerCount < playerCount)
            {
                highestPlayerCount = playerCount;
                currentHighestLevel = PortalSpawnPoints[x];
            }
        }

        if (highestPlayerCount == 0)
        {
            Debug.LogWarning("No Level Selected!");
            return null;
        }

        return currentHighestLevel.PortalInstanceData;
    }
}