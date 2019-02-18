using UnityEngine;

public class SnoccerBall : MonoBehaviour 
{
    ///The position that the ball starts and and respawns to 
    ///if something goes wrong
    [SerializeField] Vector3 RespawnPoint;

    void Awake() 
    {
        this.RespawnPoint = this.transform.position;
    }
	
    /// <summary>
    /// Respawn the ball and kill all velocity
    /// </summary>
    public void RespawnBall() 
    {
        this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
        this.transform.position = this.RespawnPoint;
    }
}
