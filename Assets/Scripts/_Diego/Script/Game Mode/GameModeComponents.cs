using System.Collections.Generic;
using SnowDay.Diego.CharacterController;
namespace SnowDay.Diego.GameMode
{
    [System.Serializable]
    public struct GameModeSettings
    {
        List<PlayerController> ActivePlayers;

        GameModeSettings(List<PlayerController> activePlayers)
        {
            ActivePlayers = activePlayers;
        }

        public List<PlayerController> GetActivePlayers()
        {
            return ActivePlayers;
        }

        public void SetActivePlayers(List<PlayerController> activePlayers)
        {
            ActivePlayers = activePlayers;
        }

    }

}