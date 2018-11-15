using UnityEngine;

/// <summary>
/// Base Class for all player
/// Allows players to take Damage
/// </summary>
public class PlayerActor : MonoBehaviour
{
    private int Health = 1;

    public int TeamID;

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            int oppTeamID = TeamID == 1 ? 0 : 1;
            TDMManager.GetInstance().IncreaseTeamScore(1, oppTeamID);
            Destroy(gameObject);
        }
    }
}