using UnityEngine;

public class SnoccerGamemodeManager : GamemodeManagerBase 
{

    [SerializeField] SnoccerTeam[] Teams = null;
    [Tooltip("The amount of goals required before a team wins.")]
    [SerializeField] uint GoalsToWin = 1;

    [System.Serializable] 
    public class SnoccerTeam : TeamBase 
    {
        public SnoccerGoal[] Goals = null;
    }

    protected override void Start() 
    {
        base.Start();

        this.SetupTeamGoals();
    }

    /// <summary>
    /// Fake event called by SnoccerGoal
    /// </summary>
    /// <param name="ScoringTeam"></param>
    public void OnTeamScore(SnoccerTeam ScoringTeam) 
    {
        ScoringTeam.Score ++;
        this.CheckGameEndConditions();
    }

    public override TeamBase[] GetTeams() 
    {
        return this.Teams;
    }

    protected override void CheckGameEndConditions() 
    {
        base.CheckGameEndConditions();
        foreach(SnoccerTeam Team in this.Teams) 
        {
            if(Team.Score >= this.GoalsToWin)
            {
                this.EndGame();    
            }
        }
    }

    protected override void CheckGameWinnerAndDisplayResults() 
    {
        if(this.Teams[0].Score > this.Teams[1].Score)
            base.GuiManager.ShowEndGameWindow("Red Team Wins!");
        else if(this.Teams[0].Score < this.Teams[1].Score)
            base.GuiManager.ShowEndGameWindow("Blue Team Wins!");
        else
            base.GuiManager.ShowEndGameWindow("Tie Game!");
    }

    /// <summary>
    /// Sets the owning teams on All SnoccarGoal scripts attached to teams.
    /// </summary>
    void SetupTeamGoals() 
    {
        foreach(SnoccerTeam Team in this.Teams) 
        {
            foreach(SnoccerGoal Goal in Team.Goals)
            {
                Goal.OwningTeam = Team;    
            }
        }
    }
}
