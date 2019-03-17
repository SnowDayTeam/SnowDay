using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Level Gate:
/// Serve as Portal Locations, will keep track of how many objects of <ObjectTag</c> are in the box
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class LevelGate : MonoBehaviour {

    [Header("Player Tag")]
    public string ObjectTag = "Player";
    [SerializeField]
    private int playersInBox;
    public bool useFriendlyName = true;
    /// <summary>
    /// How many players are currently in the collider volume.
    /// </summary>
    public int PlayersInBox
    {
        get
        {
            return playersInBox;
        }

        set
        {
            playersInBox = value;
        }
    }
    public UnityEvent OnPlayerEnterEvent;
    public UnityEvent OnPlayerleaveEvent;

    /// <summary>
    /// Portal Instance Data
    /// </summary>
    public LevelData PortalInstanceData { get; private set; }
    
    /// <summary>
    /// Initialization Function for Gate
    /// Enables Box Collider And Spawns Prefab
    /// </summary>
    /// <param name="portalData"></param>
    public void Initialize(LevelData portalData)
    {
        if (portalData == null)
        {
            Debug.LogError("Portal Data Missing in Initialization", portalData);
            return;
        }

        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        boxCollider.enabled = true;
        boxCollider.isTrigger = true;

        PortalInstanceData = portalData;

        if (!portalData.ObjectPrefab) return;

        var position = transform.position;
        Instantiate(portalData.ObjectPrefab, new Vector3(position.x, position.y, position.z), Quaternion.identity, transform);
        TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();

        if (textMesh)
        {
            if (useFriendlyName)
            {
                textMesh.text = portalData.displayName;
            }
            else
            {
                textMesh.text = portalData.sceneName;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(ObjectTag))
        {
            PlayersInBox++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ObjectTag))
        {
            PlayersInBox--;
        }
    }
}
