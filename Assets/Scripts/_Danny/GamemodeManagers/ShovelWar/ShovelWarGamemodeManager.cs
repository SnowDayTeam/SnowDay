using UnityEngine;
using SnowDay.Diego.CharacterController;

public class ShovelWarGamemodeManager : GamemodeManagerBase 
{
    [Header("GUI")]
    [Tooltip("The GUI manager for shovel war, controls text only. Attached to canvas")]
	[SerializeField] ShovelWarGUIManager ShovelWarGuiManager = null;

    [Header("Game Time")]
    [Tooltip("The time in seconds until the game is over.")]
    [Range(10.0f, 60.0f*5.0f)] [SerializeField] float GameDuration = 10.0f;

    [Header("Level Variables")]
    //The gameobjects that actualy gets shoveled
    [SerializeField] SnowPlane TeamOneSnowPlane = null;
    [SerializeField] SnowPlane TeamTwoSnowPlane = null;

    [SerializeField] ShovelWarTeam[] Teams = null;

    const float MaxSnowPlanePixels =  65536;
    bool DidGameEnd = false;

    [System.Serializable]
    public class ShovelWarTeam : Team
    {
        public SnowPlane snowPlane = null;
    }

    protected override void Start() 
    {
        base.Start();

        foreach(ShovelWarTeam team in this.Teams) 
        {
            foreach(PlayerController player in team.Players) 
            {
                player.GetComponentInChildren<SnowTackScript> ().mySnowPlane = team.snowPlane;
            }
        }
    }

    void Update() 
    {
        if(this.DidGameEnd)
            return;

        this.ShovelWarGuiManager.UpdateClearPercentTexts(
            this.TeamOneSnowPlane.RedPixelCounter / MaxSnowPlanePixels * 100, 
            this.TeamTwoSnowPlane.RedPixelCounter / MaxSnowPlanePixels * 100);

        this.CheckGameEndConditions();
        this.UpdateGameDuration();
    }

    protected override Team[] GetTeams() 
    {
        return this.Teams;
    }

    void UpdateGameDuration() 
    {
        this.GameDuration -= Time.deltaTime;
        this.ShovelWarGuiManager.UpdateGameTimeText(this.GameDuration);
    }

    void CheckGameEndConditions() 
    {
        if(this.GameDuration <= 0) 
        {
            this.CheckGameWinnerAndEndGame();
        }
    }
    
    void CheckGameWinnerAndEndGame() 
    {
        if(this.TeamOneSnowPlane.RedPixelCounter > this.TeamTwoSnowPlane.RedPixelCounter)
            this.ShovelWarGuiManager.ShowEndGameWindow("Red Team Wins!");
        else if(this.TeamOneSnowPlane.RedPixelCounter < this.TeamTwoSnowPlane.RedPixelCounter) 
            this.ShovelWarGuiManager.ShowEndGameWindow("Blue Team Wins!");
        else 
            this.ShovelWarGuiManager.ShowEndGameWindow("Tie Game!");
    }
}
