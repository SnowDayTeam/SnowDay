using UnityEngine;
using SnowDay.Diego.GameMode;
using SnowDay.Diego.CharacterController;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Manager base for all gamemodes.  This component should start off as it will 
/// be turned on by the loading screen.
/// </summary>
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

    [Header("Game Time")]
    [Tooltip("The time in seconds until the game is over. 0 means infinite game duration")]
    [SerializeField] protected float GameDuration = 1.0f;

    /// <summary>
    /// Instance of this class, WARNING this is not a singleton class, 
    /// however there should only be one instance of this class
    /// </summary>
    static public GamemodeManagerBase Instance = null;

    List<PlayerController> Players = null;

    protected bool DidGameEnd = false;
    protected bool IsInfiniteGameDuration = false;

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

    void Awake() {
        GamemodeManagerBase.Instance = this;
        this.IsInfiniteGameDuration = this.GameDuration == 0;
    }

    protected virtual void Start() 
    {
        if(this.IsInfiniteGameDuration)
            this.GuiManager.DestroyGameTimer();

        this.Players = GameModeController.GetInstance().GetActivePlayers();

        this.SetupSnowDayCamera();

        this.CreateLevelScriptsPrefabs();

        TeamBase[] teams = this.GetTeams();
        this.AssignPlayersToTeams(teams);
        this.MovePlayersToSpawnLocations(teams);
        this.SetPlayerColors(teams);
    }

    protected virtual void Update() {

        if(this.DidGameEnd)
            return;

        this.UpdateGUIScores();
        this.UpdateGameDuration();
        this.CheckGameEndConditions();
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
                player.GetComponentInChildren<SkinnedMeshRenderer> ().materials[0].SetColor("_TeamColor", team.TeamColor) ;


            }
        }
    }

    protected virtual void CheckGameEndConditions() 
    {
        if(this.IsInfiniteGameDuration)
            return;

        if(this.GameDuration <= 0.0f)
            this.EndGame();
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
        if(!this.LevelSpecificScriptsPrefab)
            return;

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

    void UpdateGameDuration() 
    {
        if(this.IsInfiniteGameDuration || this.GameDuration <= 0.0f)
            return;

        this.GameDuration -= Time.deltaTime;
        
        if(!this.GuiManager)
            return;

        this.GuiManager.UpdateGameTimeText(this.GameDuration);
    }
}
