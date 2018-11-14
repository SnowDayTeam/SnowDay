using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileComponent : MonoBehaviour
{
    public void LaunchProjectile(float speed, float angle)
    {
        float ProjectileVelocityZ = speed * Mathf.Cos(angle * Mathf.Deg2Rad);
        float ProjectileVelocityY = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        Debug.Log("Forward: " + ProjectileVelocityZ + " Vertical" + ProjectileVelocityY);

        Vector3 velocity = new Vector3(0, ProjectileVelocityY, ProjectileVelocityZ);

        Rigidbody m_rb = GetComponent<Rigidbody>();

        m_rb.AddRelativeForce(velocity, ForceMode.Impulse);
    }
}