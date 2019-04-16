using UnityEngine;
using UnityEngine.UI;

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


    //Leron Added-----------------------------------------------
    [SerializeField]
    TeamDisplay teamDisplay;
    //public GameObject img;
    //int[,] prefabValue;
   // int index = 0;
  //  public Texture[] characterImages;


    /// <summary>
    /// Current instance of this class, WARNING this is NOT a singleton class, 
    /// however there should only be one instance of this class
    /// </summary>
    static public GamemodeManagerBase Instance = null;

    public List<PlayerController> Players = null;

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
        [Tooltip("The name of the team, used to display winner during endgame.")]
        public string TeamName = "NAME ERROR";
    }
    protected bool Initialized = false;
    abstract public TeamBase[] GetTeams();
    public int GetTeamIndex(PlayerController player)
    {
        TeamBase[] team = GetTeams();
        for (int i = 0; i < team.Length; i++)
        {
            if (team[i].Players.Contains(player))
        {
                return i;
            }
        }
        return 0;
    }
    public PlayerController GetRandomPlayerFromTeam(int teamNum)
    {
        TeamBase[] team = GetTeams();
        int randIndex = Random.Range(0, team[teamNum].Players.Count);

        return team[teamNum].Players[randIndex];
    }
    protected virtual void Awake()
    {
        GamemodeManagerBase.Instance = this;
        this.IsInfiniteGameDuration = this.GameDuration == 0;
        //just in case this is left on in the inspector
        //this.enabled = false;
    }

    public virtual void Setup()
    {
        if (this.IsInfiniteGameDuration)
        {
            this.GuiManager.DestroyGameTimer();
        }

        this.Players = GameModeController.GetInstance().GetActivePlayers();

        this.SetupSnowDayCamera();

        this.CreateLevelScriptsPrefabs();

        TeamBase[] teams = this.GetTeams();
       
        this.AssignPlayersToTeams(teams);
        this.MovePlayersToSpawnLocations(teams);
        this.SetPlayerColors(teams);
        this.enabled = false;

        if (teamDisplay != null)
        {
            teamDisplay.Setup();

        }
        else
        {
            Debug.Log("Team display is null in inspector please link it,  if not in scene find it in the renderTexture folder");

        }
        LoadingScreen[] lsc = FindObjectsOfType<LoadingScreen>();
        if (lsc.Length > 1)
        {
            Debug.LogError("Ouch more than 1 loading screen script, we are having a bad time ");

        }
        lsc[0].LoadScreen();
    }

    protected virtual void Update()
    {

        if (this.DidGameEnd)
        {
            return;
        }
        if (Initialized == true)
        {
            this.UpdateGUIScores();
            this.UpdateGameDuration();
            this.CheckGameEndConditions();

        }
      
        if(Input.GetKey(KeyCode.Q)  && Input.GetKey(KeyCode.LeftControl))
        {
            this.StartCoroutine(this.LoadLevelSelect());

        }
    }

    /// <summary>
    /// Assign players to teams lineraly and as evenly as possible.
    /// </summary>
    protected void AssignPlayersToTeams(TeamBase[] Teams) 
    {
        for(int i = 0; i < this.Players.Count; i++)
        {
            Teams[i % Teams.Length].Players.Add(this.Players[i]);




            if (teamDisplay != null)
            {
                GameObject childObjects = Instantiate(teamDisplay.ImagePrefab, teamDisplay.teamPanels[i % Teams.Length].transform) as GameObject;
                // prefabValue[i, index] = player_controller.currentPrefab; //2D array going through each character on each team and getting the prefab's associated number

                //Debug.Log(prefabValue[i, index]);

                // Instantiating raw image for ever character
                childObjects.GetComponent<RawImage>().texture = teamDisplay.characterImages[Players[i].currentPrefab];
                //childObjects.transform.parent = teamPanels[i].transform; //setting it as a child under corrisponding panel color.
            }


            //----------------------------------------------------------
        }
    }

    protected void MovePlayersToSpawnLocations(TeamBase[] Teams) 
    {
        foreach(TeamBase team in Teams)
        {
            for(int i = 0 ; i < team.Players.Count; i++) 
            {
                team.Players[i].MoveCharacter(team.SpawnLocations[i % team.SpawnLocations.Length].transform.position);
                Debug.Log("moved0 " + team.SpawnLocations[i % team.SpawnLocations.Length].transform.position);
            }
        }
    }

    protected void SetPlayerColors(TeamBase[] Teams) 
    {
        foreach(TeamBase team in Teams) 
        {
            foreach (PlayerController player in team.Players)
            {
                //  player.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].SetColor("_TeamColor", team.TeamColor);
                player.GetComponentInChildren<Projector>().material = Material.Instantiate(player.GetComponentInChildren<Projector>().material);
            player.GetComponentInChildren<Projector>().material.SetColor("_Color", team.TeamColor);
//                Debug.Log("dsad "+ team.TeamColor);
            }
        }
    }

    /// <summary>
    /// Display results / win screen.
    /// </summary>
    protected virtual void CheckGameWinnerAndDisplayResults() 
    {
        List<TeamBase> WinningTeams = new List<TeamBase> ();
        int HighestScore = 0;
        foreach(TeamBase Team in this.GetTeams()) 
        {
            if(Team.Score > HighestScore) 
            {
                WinningTeams.Clear();
                WinningTeams.Add(Team);
                HighestScore = Team.Score;
            }
            else if (Team.Score == HighestScore) 
            {
                WinningTeams.Add(Team);
            }
        }
        //winner decided
        if(WinningTeams.Count == 1) 
        {
            this.GuiManager.ShowEndGameWindow(WinningTeams[0].TeamName + " Wins!");
            if (teamDisplay != null)
            {
                TeamDivisionScript tt = teamDisplay.Test.GetComponent<TeamDivisionScript>();
                foreach(EndofMatchAnimations e in tt.characters)
                {
                    e.isWinner = false;
                }

                foreach (TeamBase t in WinningTeams)
                {
                    foreach (PlayerController p in t.Players)
                    {
                        tt.characters[p.currentPrefab].isWinner = true;
                    }
                }
            }
        }
        else 
        {
            this.GuiManager.ShowEndGameWindow("Tie Game!");
        }
    }

    protected virtual void CheckGameEndConditions() 
    {
        if (this.IsInfiniteGameDuration)
        {
            return;
        }

        if(this.GameDuration <= 0.0f)
        {
            this.EndGame();
        }
            
    }

    /// <summary>
    /// Start corountine to load level select, display results.
    /// </summary>
    protected void EndGame()
    {
        this.DidGameEnd = true;
        this.CheckGameWinnerAndDisplayResults();
        this.StartCoroutine(this.LoadLevelSelect());
    }

    private void SetupSnowDayCamera() 
    {
        if (!this.SnowDayCam)
        {
            return;
        }

        this.SnowDayCam.SetTargetPlayers(GameModeController.GetInstance().GetActivePlayers());
        this.SnowDayCam.Initialize();
    }

    /// <summary>
    /// Instantiate the level scripts gameobject for all players
    /// </summary>
    private void CreateLevelScriptsPrefabs() 
    {
        if (!this.LevelSpecificScriptsPrefab)
        {
            return;
        }

        foreach(PlayerController player in this.Players)
        {
            Debug.Log(player.gameObject.name);
            Instantiate(this.LevelSpecificScriptsPrefab, player.transform.GetChild(0).GetChild(2));
        }
    }

    private IEnumerator LoadLevelSelect() 
    {
       // TeamDisplay teamDisplay = FindObjectOfType<TeamDisplay>();

        
        if (teamDisplay != null)
        {
            
           // teamDisplay.gameObject.SetActive(false);
            for (int i = 0; i < teamDisplay.transform.childCount; i++)
            {
                teamDisplay.transform.GetChild(i).gameObject.SetActive(true);
            }
        }else
        {

            Debug.Log("Team display is null in inspector please link it,  if not in scene find it in the renderTexture folder");
        }
        yield return new WaitForSeconds(this.PostGameDuration);
        //we need to change this after rewriting the GameModeController class, loading levels
        //should be done in one place only and this has many disadvantages
        UnityEngine.SceneManagement.SceneManager.LoadScene(GlobalSettingsManager.s.levelSelectScene);
    }

    private void UpdateGUIScores() 
    {
        if (!this.GuiManager)
        {
            return;
        }
            

        TeamBase[] Teams = this.GetTeams();

        for(int i = 0; i < Teams.Length; i++) 
        {
            this.GuiManager.UpdateScoreTextAtIndex(i, Teams[i].Score);
        }
    }

    void UpdateGameDuration() 
    {
        if(this.IsInfiniteGameDuration || this.GameDuration <= 0.0f)
        {
            return;
        }
           

        this.GameDuration -= Time.deltaTime;

        if (!this.GuiManager)
        {
            return;
        }
        

        this.GuiManager.UpdateGameTimeText(this.GameDuration);
    }
}
