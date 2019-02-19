using UnityEngine;
using UnityEngine.UI;
using SnowDay.Diego.CharacterController;

public class DeathmatchGamemodeManager : GamemodeManagerBase 
{
    [SerializeField] DeathMatchTeam[] Teams = null;

    [System.Serializable]
	public class DeathMatchTeam : TeamBase
    {
        public int PlayersAlive;
    }

    public int GetTeamScore(int TeamID)
    {
        return Teams[TeamID].Score;
    }

    /// <summary>
    /// Called from PlayerActor.DecreaseHealth. Happens only when a player dies
    /// </summary>
    /// <param name="DeadPlayerTeamID"> The ID of the team the player who died was part of </param>
    /// <param name="KillingPlayerTeamID"> The ID of the team who killed the player </param>
    public void OnPlayerDied(int DeadPlayerTeamID, int KillingPlayerTeamID) 
    {
        this.Teams[DeadPlayerTeamID].PlayersAlive--;
        this.Teams[KillingPlayerTeamID].Score ++;

        this.CheckGameEndConditions();
    }

    protected override void Start() 
    {
        base.Start();

        this.SetPlayerTeamIDs();
    }

    public override TeamBase[] GetTeams() 
    {
        return this.Teams;
    }

    protected override void CheckGameEndConditions() 
    {
        if(base.DidGameEnd)
            return;

        foreach(DeathMatchTeam team in this.Teams) 
        {
            if (team.PlayersAlive <= 0) 
            {
                base.EndGame();
                return;
            }
        }
    }

    protected override void CheckGameWinnerAndDisplayResults() 
    {
        //show game end screen here when it exists
    }

    private void SetPlayerTeamIDs() 
    {
        for(int i = 0; i < this.Teams.Length; i++) 
        {
            Teams[i].PlayersAlive = Teams[i].Players.Count;

            foreach (PlayerController player_controller in this.Teams[i].Players) 
            {
                player_controller.GetComponentInChildren<PlayerActor>().TeamID = i;
            }
        }
    }
}
