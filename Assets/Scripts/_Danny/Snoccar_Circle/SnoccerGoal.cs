using UnityEngine;

public class SnoccerGoal : MonoBehaviour {

    /// <summary>
    /// The team that WILL score when the ball comes into this goal.
    /// </summary>
    [HideInInspector] public SnoccerGamemodeManager.SnoccerTeam OwningTeam = null;

    void OnTriggerEnter(Collider OtherCollider) 
    {
        SnoccerBall Ball = OtherCollider.GetComponent<SnoccerBall> ();

        if(!Ball)
            return;

        //get instance of game mode manager and cast it to snoocer game mode manager
        ((SnoccerGamemodeManager)GamemodeManagerBase.Instance).OnTeamScore(this.OwningTeam);
        Ball.RespawnBall();
    }
}
