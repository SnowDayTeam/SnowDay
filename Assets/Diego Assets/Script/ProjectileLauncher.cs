using UnityEngine;

[System.Serializable]
public struct ProjectileAttribs
{

    public float speed;

    [Range (1, 180)]
    public float angle;
}

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Projectile Values")]
    public float projectileSpeedValue;

    public float projectileAngleValue;

    [Header("Projectile Prefab")]
    public GameObject projectilePrefab;

    [Header("Gizmos Settings")]
    private Vector3 gismoStartPos;
    private int arcGizmosResolution = 60;
    private Vector3[] gizmosArcPositions;
    private float renderLineOffset = 0.2f;

    Vector3 Midpoint;

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

        Vector3 velocity = new Vector3(0, ProjectileVelocityY, ProjectileVelocityZ);

        float totalTime = 2 * (ProjectileVelocityY / Physics.gravity.magnitude);

        Vector3 destinationVector = velocity * totalTime;
      
        Vector3 landingPosition = transform.position + destinationVector;

        Vector3 movementDir = landingPosition - transform.position;

        Midpoint = (transform.position + landingPosition) / 2;



        Debug.Log("Projectile Launcher - Distance Magnitude: " + destinationVector.magnitude);
        Debug.Log("Projectile Launcher - Distance DistanceBetween: " + Vector3.Distance(Vector3.zero, destinationVector));
 





        gizmosArcPositions = new Vector3[arcGizmosResolution + 1];


        float time = 2 * (ProjectileVelocityY / Physics.gravity.magnitude) + renderLineOffset;

        for (int i = 0; i < arcGizmosResolution + 1; i++)
        {
            float t = ((float)time / (float)arcGizmosResolution) * i;
            float z = gismoStartPos.z + ProjectileVelocityZ * t;
            float TimebasedonZ = z / ProjectileVelocityZ;
            double y = gismoStartPos.y + ProjectileVelocityY * TimebasedonZ + 0.5 * Physics.gravity.y * Mathf.Pow(TimebasedonZ , 2);

            gizmosArcPositions[i] =  new Vector3(gismoStartPos.x, (float)y, z);
        }

        return gizmosArcPositions;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        gismoStartPos = transform.position;
        gizmosArcPositions = CalculatePositions(projectileSpeedValue, projectileAngleValue);
        Gizmos.DrawWireSphere(Midpoint, 1);
        for (int i = 0; i < gizmosArcPositions.Length - 1; i++)
        {
            Gizmos.DrawLine(gizmosArcPositions[i], gizmosArcPositions[i + 1]);

        }
    }
}