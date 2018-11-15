using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileComponent : MonoBehaviour
{
    /// <summary>
    /// Launch Projectile
    /// </summary>
    /// <param name="speed">Projectile Speed</param>
    /// <param name="angle">Trajectory Angle</param>
    public void LaunchProjectile(float speed, float angle)
    {
        float ProjectileVelocityZ = speed * Mathf.Cos(angle * Mathf.Deg2Rad);
        float ProjectileVelocityY = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        Vector3 velocity = new Vector3(0, ProjectileVelocityY, ProjectileVelocityZ);

        Rigidbody m_rb = GetComponent<Rigidbody>();

        InstantiateDamageZone(velocity);

        m_rb.AddRelativeForce(velocity, ForceMode.Impulse);
    }

    /// <summary>
    /// Instanstates Damage Zone for Projectile
    /// </summary>
    /// <param name="velocity">Projectile Velocity</param>
    /// TODO: Player Damage | Should the Damage Zone be Instantiated when the Projectile Spawns or when it hits the ground.
    private void InstantiateDamageZone(Vector3 velocity)
    {
        float totalTime = 2 * (velocity.y / Physics.gravity.magnitude);

        Vector3 flatVelocity = velocity;

        flatVelocity.y = 0;

        Vector3 DestVector = flatVelocity * totalTime;

        Vector3 LandingPosition = transform.position + transform.forward * DestVector.magnitude;

        Instantiate(Resources.Load("DamageArea"), LandingPosition, Quaternion.identity);
    }
}