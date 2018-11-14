using UnityEngine;

[System.Serializable]
public struct ProjectileAttribs
{

    public float speed;
    public float angle;
}

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Projectile Values")]
    public float projectileSpeedValue;

    public float projectileAngleValue;

    [Header("Projectile Prefab")]
    public GameObject projectilePrefab;

    [Header("Gizmo Settings")]
    private Vector3 gismoStartPos;
    private int arcGizmosResolution = 60;
    private Vector3[] gizmosArcPositions;

    /// <summary>
    /// Launch Projectile
    /// </summary>
    /// <param name="attribs">Projectile Attributes</param>
    public void LaunchProjectile(ProjectileAttribs attribs)
    {
        ProjectileComponent proj = Instantiate(projectilePrefab, transform.position, transform.rotation,null).GetComponent<ProjectileComponent>();
        proj.LaunchProjectile(attribs.speed, attribs.angle);
        projectileSpeedValue = attribs.speed;
        projectileAngleValue = attribs.angle;
    }

    /// <summary>
    /// Calculates the Points in the Projectile Arc
    /// </summary>
    /// <param name="speed">Speed of the projectile.</param>
    /// <param name="angle">Angle of the trajectory</param>
    /// <param name="resolution"></param>
    /// <returns></returns>
    private Vector3[] CalculatePositions(float speed, float angle)
    {
        float ProjectileVelocityZ = speed * Mathf.Cos(angle * Mathf.Deg2Rad);
        float ProjectileVelocityY = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        gizmosArcPositions = new Vector3[arcGizmosResolution + 1];

        float time = 2 * (ProjectileVelocityY / Physics.gravity.magnitude);

        for (int i = 0; i < arcGizmosResolution + 1; i++)
        {
            float t = ((float)time / (float)arcGizmosResolution) * i;
            float z = gismoStartPos.z + ProjectileVelocityZ * t;
            double y = gismoStartPos.y + ProjectileVelocityY * t + 0.5 * Physics.gravity.y * Mathf.Pow(t, 2);

            gizmosArcPositions[i] = new Vector3(gismoStartPos.x, (float)y, z);
        }

        return gizmosArcPositions;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        gismoStartPos = transform.position;
        gizmosArcPositions = CalculatePositions(projectileSpeedValue, projectileAngleValue);
        for (int i = 0; i < gizmosArcPositions.Length - 1; i++)
        {
            Gizmos.DrawLine(gizmosArcPositions[i], gizmosArcPositions[i + 1]);

        }
    }
}