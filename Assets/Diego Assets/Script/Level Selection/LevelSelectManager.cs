using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Level Selection Manager
/// </summary>
/// <remarks>
/// <para>Counts down from <c>LevelSelectCountDownTime</c> and changes to scene selected by players</para>
/// </remarks>
/// 
public class LevelSelectManager : MonoBehaviour
{
    [Header("Countdown Timer")]
    public float LevelSelectCountDownTime;

    private float timeElapsed;

    [Header("UI Text")]
    public Text CountDownText;

    [Header("Portal Spawn Locations")]
    public List<LevelGate> PortalSpawnPoints;

    [Header("Game Levels")]
    public List<LevelData> Levels;

    // Use this for initialization
    //TODO: Make Random level select
    private void Start()
    {
        timeElapsed = LevelSelectCountDownTime;
        for (var x = 0; x < PortalSpawnPoints.Capacity; x++)
        {
            if (!Levels[x])
            {
                Debug.LogError("Missing Level Data");
                continue;
            }
            PortalSpawnPoints[x].Initialize(Levels[x]);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        timeElapsed -= Time.deltaTime;
        if (timeElapsed <= 0)
        {
            LevelData selectedLevel = NextLevelSelect();
            if (selectedLevel)
            {
                SceneManager.LoadScene(selectedLevel.SceneName);
            }
            return;
        }
        CountDownText.text = timeElapsed.ToString("0.##");
    }

    /// <summary>
    /// Checks what Gate has the most amount of players and returns the LevelData" linked to that portal.
    /// </summary>
    /// <returns></returns>
    private LevelData NextLevelSelect()
    {
        LevelGate currentHighestLevel = null;
        var highestPlayerCount = 0;

        for (int x = 0; x < PortalSpawnPoints.Count; x++)
        {
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