using UnityEngine;

/// <summary>
/// Damage Area for all Projectiles
/// Checks if Player is in radius around target and causes damage
/// </summary>
public class DamageArea : MonoBehaviour
{
    public float sphereRadius = 1;

    // Update is called once per frame
    private void Update()
    {
        Collider[] sphereHits;
        sphereHits = Physics.OverlapSphere(transform.position, sphereRadius);
        foreach (var item in sphereHits)
        {
            if (item.tag == "Player")
            {
                item.SendMessage("TakeDamage", 1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}