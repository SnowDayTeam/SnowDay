using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toboggan
{

    public class FinishManager : MonoBehaviour
    {
        public static List<Transform> players;
        public static FinishManager _Instance;
        // Use this for initialization
        public static void Register(Transform player)
        {
            if (players == null)
            {
                players = new List<Transform>(); 
            }
            players.Add(player);
        }
        public static void Remove(Transform player)
        {
            players.Remove(player); 
            if (players.Count == 1)
            {
                _Instance.GameOver();
            }

        }
        void Awake()
        {
            _Instance = this;
        }
        public void GameOver()
        {
            Debug.Log("game over");
        }
        void Update()
        {

        }
    }
}