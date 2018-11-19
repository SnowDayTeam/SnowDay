using UnityEditor;
using UnityEngine;

/// <summary>
/// Projectile Attributes
/// float Speed - Bullet Speed
/// float Angle - Angle of Trajectory
/// </summary>
[System.Serializable]
public struct ProjectileAttribs
{
    public float speed;

    [Range(1, 90)]
    public float angle;
}

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Projectile Prefab")]
    public GameObject projectilePrefab;

    [Header("Projectile Values")]
    private float projectileSpeedValue;

    private float projectileAngleValue;

    //Charge Shot Variables
    private bool charging = false;

    private float fireAngle = 0f;

    public int chargeSpeed = 10;
    public int maxChargeAngle = 90;

    //Gizmo Variables
    private Vector3 gismoStartPos;

    private Vector3[] gizmosArcPositions;

    //Gizmo Constants
    private const int arcGizmosResolution = 60;

    private const float renderLineOffset = 0.2f;

    /// <summary>
    /// Launch Projectile
    /// </summary>
    /// <param name="attribs">Projectile Attributes</param>
    public void LaunchProjectile(ProjectileAttribs attribs)
    {
        ProjectileComponent proj = Instantiate(projectilePrefab, transform.position, transform.rotation, null).GetComponent<ProjectileComponent>();
        proj.LaunchProjectile(attribs.speed, attribs.angle);
        projectileSpeedValue = attribs.speed;
        projectileAngleValue = attribs.angle;
    }

    /// <summary>
    /// Launch Charge projectile
    /// Will increase angle while player is holding key down until it reaches the max angle.
    /// <!Note> Will Ignore Projectile Attribs Angle<!Note>
    /// </summary>
    /// <param name="attribs"></param>
    /// <param name="pressed"></param>
    public void LaunchChargeProjectile(ProjectileAttribs attribs, bool pressed)
    {
        projectileAngleValue = fireAngle;
        if (pressed && !charging)
        {
            charging = true;
            projectileSpeedValue = attribs.speed;
        }
        else
        {
            if (pressed)
            {
                return;
            }
            if (!pressed && charging)
            {
                charging = false;
                ProjectileComponent proj = Instantiate(projectilePrefab, transform.position, transform.rotation, null).GetComponent<ProjectileComponent>();
                proj.LaunchProjectile(attribs.speed, fireAngle);
                fireAngle = 0;
            }
        }
    }

    private void Update()
    {
        if (charging)
        {
            fireAngle += chargeSpeed * Time.deltaTime;
            fireAngle = Mathf.Clamp(fireAngle, 0, maxChargeAngle);
            //Debug.Log(fireAngle);
        }
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

        gizmosArcPositions = new Vector3[arcGizmosResolution + 1];

        float time = 2 * (ProjectileVelocityY / Physics.gravity.magnitude) + renderLineOffset;

        for (int i = 0; i < arcGizmosResolution + 1; i++)
        {
            float t = ((float)time / (float)arcGizmosResolution) * i;

            float curDistance = ProjectileVelocityZ * t;
            float TimebasedonZ = curDistance / ProjectileVelocityZ;
            double y = gismoStartPos.y + ProjectileVelocityY * TimebasedonZ + 0.5 * Physics.gravity.y * Mathf.Pow(TimebasedonZ, 2);

            Vector3 ArcPoint = gismoStartPos + transform.forward * curDistance;
            ArcPoint.y = (float)y;

            gizmosArcPositions[i] = ArcPoint;
        }

        return gizmosArcPositions;
    }

    /// <summary>
    /// Draws Projectile Arc in Editor
    /// Only During Play Mode
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (EditorApplication.isPlaying)
        {
            gismoStartPos = transform.position;
            gizmosArcPositions = CalculatePositions(projectileSpeedValue, projectileAngleValue);
            for (int i = 0; i < gizmosArcPositions.Length - 1; i++)
            {
                Gizmos.DrawLine(gizmosArcPositions[i], gizmosArcPositions[i + 1]);
            }
        }
    }
}