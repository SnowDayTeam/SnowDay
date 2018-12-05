using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SnowDay.Diego.Singleton;
using SnowDay.Diego.GameMode;
using SnowDay.Diego.CharacterController;

namespace SnowDay.Diego.LevelSelect
{
    /// <summary>
    /// Level Selection Manager
    /// </summary>
    /// <remarks>
    /// <para>Counts down from <c>LevelSelectCountDownTime</c> and changes to scene selected by players</para>
    /// </remarks>
    ///
    [RequireComponent(typeof(LevelSelectPlayerActivator))]
    public class LevelSelectManager : Singleton<LevelSelectManager>
    {
        public bool EnableTimer = true;

        public SnowDayCamera cam;

        [Header("Countdown Timer")]
        public float LevelSelectCountDownTime;

        private float timeElapsed;

        [Header("UI Text")]
        public Text CountDownText;

        [Header("Portal Spawn Locations")]
        public List<LevelGate> PortalSpawnPoints;

        [Header("Game Levels")]
        public List<LevelData> Levels;

        private LevelSelectPlayerActivator playerActivator;

        // Use this for initialization
        //TODO: Make Random level select
        private void Start()
        {
            //Initialize
            playerActivator = GetComponent<LevelSelectPlayerActivator>();
            cam.Initialize();

            timeElapsed = LevelSelectCountDownTime;
            if (!EnableTimer)
            {
                CountDownText.enabled = false;
            }
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
            if (EnableTimer)
            {
                timeElapsed -= Time.deltaTime;
                if (timeElapsed <= 0)
                {
                    LevelData selectedLevel = NextLevelSelect();
                    if (selectedLevel)
                    {
                        SaveActivePlayers();
                        GameModeController.GetInstance().LoadGameMode(selectedLevel);
                        EnableTimer = false;
                    }
                    return;
                }
                CountDownText.text = timeElapsed.ToString("0.##");
            }
        }

           

        private void SaveActivePlayers()
        {
            List<PlayerController> players = playerActivator.GetActivePlayers();
            GameModeController.GetInstance().SetActivePlayers(players);
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
}