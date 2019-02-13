using UnityEngine;
using SnowDay.Diego.GameMode;
using SnowDay.Diego.CharacterController;
using System.Collections.Generic;
using System.Collections;

abstract public class GamemodeManagerBase : MonoBehaviour 
{
    [Tooltip("The scripts that create the gameplay for this gamemode")]
    [SerializeField] GameObject LevelSpecificScriptsPrefab;
    [Tooltip("The gamemode camera that follows players around.")]
    [SerializeField] SnowDayCamera SnowDayCam;
    [Tooltip("The time in seconds before the level select is loaded again.")]
    [SerializeField] float PostGameDuration = 1.0f;

    List<PlayerController> Players = null;

    protected bool DidGameEnd = false;

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
    /// <summary>
    /// Display results / win screen.
    /// </summary>
    abstract protected void CheckGameWinnerAndDisplayResults();
    /// <summary>
    /// This is abstract simply to streamline derrived classes.
    /// </summary>
    abstract protected void CheckGameEndConditions();

    protected virtual void Start() 
    {
        this.Players = GameModeController.GetInstance().GetActivePlayers();

        this.SetupSnowDayCamera();

        this.CreateLevelScriptsPrefabs();

        Team[] teams = this.GetTeams();
        this.AssignPlayersToTeams(teams);
        this.MovePlayersToSpawnLocations(teams);
        this.SetPlayerColors(teams);
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

    /// <summary>
    /// Start corountine to load level select, display results.
    /// </summary>
    protected void EndGame() {
        this.DidGameEnd = true;
        this.CheckGameWinnerAndDisplayResults();
        this.StartCoroutine(this.LoadLevelSelect());
    }

    private void SetupSnowDayCamera() 
    {
        if(!this.SnowDayCam)
            return;

        this.SnowDayCam.SetTargetPlayers(GameModeController.GetInstance().GetActivePlayers());
        this.SnowDayCam.Initialize();
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

    private IEnumerator LoadLevelSelect() 
    {
        yield return new WaitForSeconds(this.PostGameDuration);
        //we need to change this after rewriting the GameModeController class, loading levels
        //should be done in one place only and this has many disadvantages
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }

}
