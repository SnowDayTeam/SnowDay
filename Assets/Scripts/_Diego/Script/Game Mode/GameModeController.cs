using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SnowDay.Diego.Singleton;
using SnowDay.Diego.CharacterController;

namespace SnowDay.Diego.GameMode
{
    class GameModeController : Singleton<GameModeController>
    {
        GameModeSettings gameMode;

        private void Awake()
        {
            DestroyOnLoad = false;
        }

        public void SetActivePlayers(List<PlayerController> activePlayers)
        {
            gameMode.SetActivePlayers(activePlayers);
        }

        public List<PlayerController> GetActivePlayers()
        {
            return gameMode.GetActivePlayers();
        }

        private void MoveActivePlayerToScene(LevelData selectedLevel)
        {
            Scene scene = SceneManager.GetSceneByName(selectedLevel.sceneName);
            foreach (var player in gameMode.GetActivePlayers())
            {
                SceneManager.MoveGameObjectToScene(player.gameObject, scene);
            }
        }

        public void LoadGameMode(LevelData selectedLevel)
        {
            Scene LevelSelectScene = SceneManager.GetActiveScene();

            SceneManager.LoadSceneAsync(selectedLevel.sceneName, LoadSceneMode.Additive);

            MoveActivePlayerToScene(selectedLevel);

            UnloadScene(LevelSelectScene);

        }

        private void UnloadScene(Scene scene)
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            foreach (var item in gameObjects)
            {
                Destroy(item);
            }

            SceneManager.UnloadSceneAsync(scene);
        }
    }
}
