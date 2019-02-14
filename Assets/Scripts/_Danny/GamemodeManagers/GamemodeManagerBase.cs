using UnityEngine;
using SnowDay.Diego.GameMode;
using SnowDay.Diego.CharacterController;
using System.Collections.Generic;
using System.Collections;

// Note: if all gamemodes have a timer move time to this class
abstract public class GamemodeManagerBase : MonoBehaviour 
{

    [Header("GUI")]
    [Tooltip("The GUI manager for this gamemode, controls text only. Attached to canvas")]
	[SerializeField] protected GamemodeGUIManagerBase GuiManager = null;

    [Header("Level Variables")]
    [Tooltip("The scripts that create the gameplay for this gamemode")]
    [SerializeField] GameObject LevelSpecificScriptsPrefab;
    [Tooltip("The gamemode camera that follows players around.")]
    [SerializeField] SnowDayCamera SnowDayCam;
    [Tooltip("The time in seconds before the level select is loaded again.")]
    [SerializeField] float PostGameDuration = 1.0f;

    List<PlayerController> Players = null;

    protected bool DidGameEnd = false;

    [System.Serializable]
    public class TeamBase 
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

    abstract protected TeamBase[] GetTeams();
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

        TeamBase[] teams = this.GetTeams();
        this.AssignPlayersToTeams(teams);
        this.MovePlayersToSpawnLocations(teams);
        this.SetPlayerColors(teams);
    }

    protected virtual void Update() {
        this.UpdateGUIScores();
    }

    /// <summary>
    /// Assign players to teams lineraly and as evenly as possible.
    /// </summary>
    protected void AssignPlayersToTeams(TeamBase[] Teams) 
    {
        for(int i = 0; i < this.Players.Count; i++) {
            Teams[i % Teams.Length].Players.Add(this.Players[i]);
        }
    }

    protected void MovePlayersToSpawnLocations(TeamBase[] Teams) 
    {
        foreach(TeamBase team in Teams) {
            for(int i = 0 ; i < team.Players.Count; i++) 
            {
                team.Players[i].MoveCharacter(team.SpawnLocations[i % team.SpawnLocations.Length].transform.position);
            }
        }
    }

    protected void SetPlayerColors(TeamBase[] Teams) 
    {
        foreach(TeamBase team in Teams) 
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

    private void UpdateGUIScores() 
    {
        if(!this.GuiManager)
            return;

        TeamBase[] Teams = this.GetTeams();

        for(int i = 0; i < Teams.Length; i++) 
        {
            this.GuiManager.UpdateScoreTextAtIndex(i, Teams[i].Score);
        }
    }
}
