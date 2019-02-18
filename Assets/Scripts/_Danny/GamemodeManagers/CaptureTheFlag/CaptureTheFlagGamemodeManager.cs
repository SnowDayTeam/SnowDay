using UnityEngine;
using SnowDay.Diego.CharacterController;

public class CaptureTheFlagGamemodeManager : GamemodeManagerBase {

    [SerializeField] TeamBase[] Teams = null; 

    //Either Flag spawning should happen here or this 
    //should be moved to flag spawner
    [Header("Flag Variables")]
    public int MaxFlags = 2;
    public int CurrentFlags = 1;

    //refactor flag pickup and get rid of these
    [HideInInspector] public float RedTeamScore;
    [HideInInspector] public float BlueTeamScore;

    protected override void Start() {
        base.Start();

        this.SetPlayerTeamIDs();
    }

    protected override void Update() {
        
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
        if(this.RedTeamScore > this.BlueTeamScore)
            base.GuiManager.ShowEndGameWindow("Red Team Wins!");
        else if(this.RedTeamScore < this.BlueTeamScore) 
            base.GuiManager.ShowEndGameWindow("Blue Team Wins!");
        else 
            base.GuiManager.ShowEndGameWindow("Tie Game!");
    }

    private void SetPlayerTeamIDs() 
    {
        for(int i = 0; i < this.Teams.Length; i++) 
        {
            foreach(PlayerController player_controller in this.Teams[i].Players) 
            {
                player_controller.GetComponentInChildren<FlagController>().team = i;
            }
        }
    }

    private void UpdateGameDuration() 
    {
        this.GameDuration -= Time.deltaTime;
        base.GuiManager.UpdateGameTimeText(this.GameDuration);
    }

    private void UpdateTeamScores() {

        this.Teams[0].Score = (int)this.RedTeamScore;
        this.Teams[1].Score = (int)this.BlueTeamScore;
    }
}
