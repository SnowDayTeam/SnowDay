using UnityEngine;
using SnowDay.Diego.CharacterController;

public class ShovelWarGamemodeManager : GamemodeManagerBase 
{
    //The gameobjects that actualy gets shoveled
    [SerializeField] SnowPlane TeamOneSnowPlane = null;
    [SerializeField] SnowPlane TeamTwoSnowPlane = null;

    [SerializeField] ShovelWarTeam[] Teams = null;

    const float MaxSnowPlanePixels = 65536;

    [System.Serializable]
    public class ShovelWarTeam : TeamBase
    {
        public SnowPlane snowPlane = null;
    }

    public override void Setup() 
    {
        base.Setup();

        foreach(ShovelWarTeam team in this.Teams) 
        {
            foreach(PlayerController player in team.Players) 
            {
                player.GetComponentInChildren<SnowTackScript> ().mySnowPlane = team.snowPlane;
            }
        }
        Initialized = true;

    }

    protected override void Update() 
    {
        if(base.DidGameEnd)
            return;

        this.UpdateTeamScores();
        this.CheckGameEndConditions();
        base.Update();
    }

    public override TeamBase[] GetTeams() 
    {
        return this.Teams;
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

    void UpdateTeamScores() {

        this.Teams[0].Score = Mathf.RoundToInt(this.TeamOneSnowPlane.RedPixelCounter / MaxSnowPlanePixels * 100);
        this.Teams[1].Score = Mathf.RoundToInt(this.TeamTwoSnowPlane.RedPixelCounter / MaxSnowPlanePixels * 100);
    }
}
