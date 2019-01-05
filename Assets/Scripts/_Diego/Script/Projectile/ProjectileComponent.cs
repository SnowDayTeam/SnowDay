using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileComponent : MonoBehaviour
{
    public PlayerActor playerActor;
    [Header("Destroy Projectile")]
    public float timeUntilDestroy = 2;

    [Header("Damage area radius")]
    public bool increaseOverTime;
    public float MaxRadius = 1;
    public float RadiusIncreaseStep = 1;

    private float currentRadius = 0;
    private bool increaseRadius = true;

    /// <summary>
    /// Launch Projectile
    /// </summary>
    /// <param name="speed">Projectile Speed</param>
    /// <param name="angle">Trajectory Angle</param>
    public void LaunchProjectile(float speed, float angle, Vector3 dir)
    {
        float ProjectileVelocityZ = speed * Mathf.Cos(angle * Mathf.Deg2Rad);
        float ProjectileVelocityY = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

        Vector3 velocity = new Vector3(0, ProjectileVelocityY, ProjectileVelocityZ);

        Rigidbody m_rb = GetComponent<Rigidbody>();


        m_rb.AddRelativeForce(velocity, ForceMode.Impulse);
    }



    private void Start()
    {
        if (!increaseOverTime)
        {
            currentRadius = MaxRadius;
            increaseRadius = false;
        }
    }

    //private void Update()
    //{
    //    if (increaseRadius)
    //    {
    //        currentRadius += RadiusIncreaseStep * Time.deltaTime;
    //    }
    //    Collider[] sphereHits;
    //    sphereHits = Physics.OverlapSphere(transform.position, currentRadius);
    //    foreach (var item in sphereHits)
    //    {
    //        PlayerActor act = item.GetComponentInChildren<PlayerActor>();
    //        if(act != null)
    //        {
    //            if(act != playerActor)
    //            {
    //               // print("got hit");
    //             //   Debug.Log(item.gameObject.name);
    //            }
    //        }
    //        //if (item.tag == "Player")
    //        //{
    //        //    item.SendMessage("TakeDamage", 1);
    //        //}
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        increaseRadius = false;
        PlayerActor act = collision.collider.GetComponentInChildren<PlayerActor>();
        if (act != null)
        {
            if (act != playerActor)
            {
                print("got hit");
                Debug.Log(collision.gameObject.name);
                act.DecreaseHealth(1, playerActor.TeamID);
            }
        }
        Destroy(gameObject, timeUntilDestroy);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }
}