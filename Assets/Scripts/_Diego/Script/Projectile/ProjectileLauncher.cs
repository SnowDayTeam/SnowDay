using UnityEditor;
using UnityEngine;
using System.Collections;

using SnowDay.Diego.CharacterController;
using SnowDay.Diego.GameMode;

/// <summary>
/// Projectile Attributes
/// float Speed - Bullet Speed
/// float Angle - Angle of Trajectory
/// </summary>
[System.Serializable]
public struct ProjectileAttribs
{
    //[Range(1, 600)]
    public float speed;

    [Range(1, 90)]
    public float angle;

    public ProjectileAttribs(float Speed, float Angle)
    {
        speed = Speed;
        angle = Angle;
    }
}

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Projectile Prefab")]
    public GameObject projectilePrefab = null;

    [Header("Bullet Modes")]
    public ProjectileAttribs DefaultShot = new ProjectileAttribs(20f, 100f);

    public ProjectileAttribs ChargeShot = new ProjectileAttribs(10f, 45f);

    [Header("Charge Shoot")]
    [Tooltip("Projectile charging Speed")]
    public int chargeSpeed = 5;

    [Tooltip("Max projectile charging Speed")]
    public int maxChargeSpeed = 10;

    [Header("Aim Assists Radius")]
    private Vector3 AimAssistLandingPos = Vector3.zero;
    public float AimAssistRadius = 0.5f;

    //Charge Shot Variables
    private bool charging = false;

    private float fireVelocity = 0f;

    [Header("Projectile Values")]
    private float projectileSpeedValue = 0;

    private float projectileAngleValue = 0;

    //Gizmo's Variables
    private Vector3 gismoStartPos = Vector3.zero;

    private Vector3[] gizmosArcPositions = null;

    //Gizmo's Constants
    private const int arcGizmosResolution = 60;

    private const float renderLineOffset = 0.2f;

    //player ammo variables
    public int playerAmmo = 5;

    //auto aim angle
    public float autoAimAngle = 25.0f;

    /// <summary>
    /// Launch Projectile
    /// </summary>
    /// <param name="attribs">Projectile Attributes</param>
    public void LaunchProjectile(PlayerActor actor)
    {
        if (playerAmmo > 0)
        {
            ProjectileComponent proj = Instantiate(projectilePrefab, transform.position, transform.rotation, null).GetComponent<ProjectileComponent>();
            proj.playerActor = actor; 
             playerAmmo--;

            //get reference to all players
            //var AllPlayers = GameModeController.GetInstance().GetActivePlayers();
            //for (int i = 0; i < AllPlayers.Count; i++)
            //{
            //    if (AllPlayers[i].GetComponentInChildren<PlayerActor>().TeamID == actor.TeamID)
            //    {
            //        //Debug.Log("check enemies");
            //        float angleBetweenPlayers = Vector3.Angle(AllPlayers[i].GetComponentInChildren<PlayerActor>().transform.position, actor.transform.forward);

            //     //   Debug.Log("Angle Between players: " + angleBetweenPlayers);
            //        if(angleBetweenPlayers < autoAimAngle)
            //        {

            //           // DefaultShot.angle = angleBetweenPlayers; 
            //            Debug.Log("aim corrected"); 
            //        }
            //    else
            //        {
            //            Debug.Log("standard aim");
            //        }
            //    }
            //}

            proj.LaunchProjectile(DefaultShot.speed, DefaultShot.angle, transform.forward);

            projectileSpeedValue = DefaultShot.speed;
            projectileAngleValue = DefaultShot.angle;
        }
        else
        {
            StartCoroutine(reload());
        }
    }

    private IEnumerator reload()
    {
        playerAmmo = 0; 
          yield return new WaitForSeconds(3);
        playerAmmo = 5;
    }

    /// <summary>
    /// Launch Charge projectile
    /// Will increase angle while player is holding key down until it reaches the max angle.
    /// </summary>
    /// <param name="attribs"></param>
    /// <param name="pressed"></param>
    public void LaunchChargeProjectile(bool pressed)
    {
        if (pressed && !charging)
        {
            charging = true;
            projectileAngleValue = ChargeShot.angle;
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
                projectileSpeedValue = ChargeShot.speed + fireVelocity;
                proj.LaunchProjectile(projectileSpeedValue, projectileAngleValue, CalculateLandingPos());
                fireVelocity = 0;
            }
        }
    }

    private void Update()
    {
        if (charging)
        {
            Debug.Log("Charge Shoot");
            fireVelocity += chargeSpeed * Time.deltaTime;
            fireVelocity = Mathf.Clamp(fireVelocity, 0, maxChargeSpeed);
            //Debug.Log(fireAngle);
        }
    }

    public Vector3 CalculateLandingPos()
    {
        Vector3 destinationTarget = new Vector3();

        float ProjectileVelocityZ = projectileSpeedValue * Mathf.Cos(projectileAngleValue * Mathf.Deg2Rad);
        float ProjectileVelocityY = projectileSpeedValue * Mathf.Sin(projectileAngleValue * Mathf.Deg2Rad);

        Vector3 velocity = new Vector3(0, ProjectileVelocityY, ProjectileVelocityZ);

        float totalTime = 2 * (ProjectileVelocityY / Physics.gravity.magnitude);

        Vector3 distanceTraveled = velocity * totalTime;

        distanceTraveled.y = 0;
  
        AimAssistLandingPos = transform.position + transform.forward * distanceTraveled.magnitude;

        Collider[] sphereHits = Physics.OverlapSphere(AimAssistLandingPos, AimAssistRadius );
        for (int i = 0; i < sphereHits.Length; i++)
        {
            if (sphereHits[i].tag == "Player")
            {
                Debug.Log(sphereHits[i].name);
                destinationTarget = sphereHits[i].transform.position - transform.position;
                //sphereHits[i].SendMessage("TakeDamage", 1);
                break;
            }
        }

        return destinationTarget;





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
        Gizmos.color = Color.blue;
        if (EditorApplication.isPlaying)
        {
            gismoStartPos = transform.position;
            gizmosArcPositions = CalculatePositions(projectileSpeedValue, projectileAngleValue);
            for (int i = 0; i < gizmosArcPositions.Length - 1; i++)
            {
                Gizmos.DrawLine(gizmosArcPositions[i], gizmosArcPositions[i + 1]);
            }
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(AimAssistLandingPos, AimAssistRadius);
        }
    }
}