using UnityEngine;
using SnowDay.Diego.CharacterController;

public class ShovelWarGamemodeManager : GamemodeManagerBase 
{
    //The gameobjects that actualy gets shoveled
    [SerializeField] SnowPlane TeamOneSnowPlane = null;
    [SerializeField] SnowPlane TeamTwoSnowPlane = null;

    [SerializeField] ShovelWarTeam[] Teams = null;

    [Header("Game Time")]
    [Tooltip("The time in seconds until the game is over.")]
    [Range(10.0f, 60.0f*5.0f)] [SerializeField] float GameDuration = 10.0f;

    const float MaxSnowPlanePixels =  65536;

    [System.Serializable]
    public class ShovelWarTeam : TeamBase
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

    protected override void Update() 
    {
        if(base.DidGameEnd)
            return;

        this.UpdateTeamScores();
        this.CheckGameEndConditions();
        this.UpdateGameDuration();
        base.Update();
    }

    protected override TeamBase[] GetTeams() 
    {
        return this.Teams;
    }

    protected override void CheckGameEndConditions() 
    {
        if(this.GameDuration <= 0) 
        {
            base.EndGame();
        }
    }
    
    protected override void CheckGameWinnerAndDisplayResults()
    {
        if(this.TeamOneSnowPlane.RedPixelCounter > this.TeamTwoSnowPlane.RedPixelCounter)
            base.GuiManager.ShowEndGameWindow("Red Team Wins!");
        else if(this.TeamOneSnowPlane.RedPixelCounter < this.TeamTwoSnowPlane.RedPixelCounter) 
            base.GuiManager.ShowEndGameWindow("Blue Team Wins!");
        else 
            base.GuiManager.ShowEndGameWindow("Tie Game!");
    }

    void UpdateGameDuration() 
    {
        this.GameDuration -= Time.deltaTime;
        base.GuiManager.UpdateGameTimeText(this.GameDuration);
    }

    void UpdateTeamScores() {

        //this game mode will only ever have two teams in this order
        this.Teams[0].Score = Mathf.RoundToInt(this.TeamOneSnowPlane.RedPixelCounter / MaxSnowPlanePixels * 100);
        this.Teams[1].Score = Mathf.RoundToInt(this.TeamTwoSnowPlane.RedPixelCounter / MaxSnowPlanePixels * 100);
    }
}
