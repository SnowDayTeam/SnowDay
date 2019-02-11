using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SnowDay.Diego.Singleton;
using SnowDay.Diego.CharacterController;

namespace SnowDay.Diego.GameMode
{
    class GameModeController : Singleton<GameModeController>
    {
        //why does this even exist?
        //why is this a struct
        GameModeSettings gameMode;

        private void Awake()
        {
            DestroyOnLoad = false;
            SetActivePlayers(new List<PlayerController>());
            if (GameModeController.GetInstance() != this)
            {
                Debug.Log("destroyed");
                Destroy(this.gameObject);
            }
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
