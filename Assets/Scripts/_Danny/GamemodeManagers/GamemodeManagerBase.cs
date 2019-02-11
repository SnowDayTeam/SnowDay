using UnityEngine;
using SnowDay.Diego.GameMode;
using SnowDay.Diego.CharacterController;
using System.Collections.Generic;
using System.Collections;

abstract public class GamemodeManagerBase : MonoBehaviour 
{
    [Tooltip("The scripts attached to this prefab that create the gameplay for this gamemode")]
    [SerializeField] GameObject LevelSpecificScriptsPrefab;
    [Tooltip("The time in seconds before the level select is loaded again.")]
    [SerializeField] float PostGameDuration = 1.0f;

    List<PlayerController> Players = null;

    [System.Serializable]
    abstract public class Team 
    {
        /// The players on this team
        public List<PlayerController> Players = null;
        /// Spawn locations for this team
        public SpawnLocation[] SpawnLocations = null;
        /// This teams' score
        public int Score = 0;
        /// The color of this team
        public Color TeamColor = Color.black;
    }

    abstract protected Team[] GetTeams();

    protected virtual void Start() 
    {
        this.Players = GameModeController.GetInstance().GetActivePlayers();

        this.CreateLevelScriptsPrefabs();

        Team[] teams = this.GetTeams();
        this.AssignPlayersToTeams(teams);
        this.MovePlayersToSpawnLocations(teams);
        this.SetPlayerColors(teams);
    }

    /// <summary>
    /// Instantiate the level scripts gameobject for all players
    /// </summary>
    private void CreateLevelScriptsPrefabs() 
    {
        foreach(PlayerController player in this.Players)
        {
            Debug.Log(player.gameObject.name);
            Instantiate(this.LevelSpecificScriptsPrefab, player.transform.GetChild(0).GetChild(2));
        }
    }

    /// <summary>
    /// Assign players to teams lineraly and as evenly as possible.
    /// </summary>
    protected void AssignPlayersToTeams(Team[] Teams) 
    {
        for(int i = 0; i < this.Players.Count; i++) {
            Teams[i % Teams.Length].Players.Add(this.Players[i]);
        }
    }

    protected void MovePlayersToSpawnLocations(Team[] Teams) 
    {
        foreach(Team team in Teams) {
            for(int i = 0 ; i < team.Players.Count; i++) 
            {
                //if you get an error here its becasue you dont have enough spawn locations for your team
                team.Players[i].MoveCharacter(team.SpawnLocations[i].transform.position);
            }
        }
    }

    protected void SetPlayerColors(Team[] Teams) 
    {
        foreach(Team team in Teams) 
        {
            foreach(PlayerController player in team.Players) 
            {
                player.GetComponentInChildren<SkinnedMeshRenderer> ().materials[0].color = team.TeamColor;
            }
        }
    }

    protected IEnumerator LoadLevelSelect() {
        yield return new WaitForSeconds(this.PostGameDuration);

        GameModeController.GetInstance().load

    }
}
