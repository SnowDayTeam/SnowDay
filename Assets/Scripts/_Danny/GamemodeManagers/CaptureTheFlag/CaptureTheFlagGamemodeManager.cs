using UnityEngine;
using SnowDay.Diego.CharacterController;

public class CaptureTheFlagGamemodeManager : GamemodeManagerBase
{

    [SerializeField] CaptureTheFlagTeam[] Teams = null;

    [System.Serializable]
    public class CaptureTheFlagTeam : TeamBase
    {
        public GoalZone Goal;
    }

    public override TeamBase[] GetTeams() 
    {
        return this.Teams;
    }

    protected override void Start() {
        base.Start();

        this.SetupGoalZones();
    }

    //protected override void Start()
    //{
    //    base.Start();

    //this.SetPlayerTeamIDs();
    //}

    protected override void Update()
    {
        if (base.DidGameEnd)
        {
            return;
        }

        base.Update();
    }

    //private void SetPlayerTeamIDs() 
    //{
    //    for(int i = 0; i < this.Teams.Length; i++) 
    //    {
    //        foreach(PlayerController player_controller in this.Teams[i].Players) 
    //        {
    //            //player_controller.GetComponentInChildren<FlagController>().team = i;
    //        }
    //    }
    //}

    private void SetupGoalZones() 
    {
        foreach(CaptureTheFlagTeam Team in this.Teams) 
        {
            Team.Goal.OwningTeam = Team;
        }
    }
}
