using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(BoxCollider))]
public class LevelPortal : MonoBehaviour {

    [Header("Player Tag")]
    public string PlayerTag = "Player";

    [Header ("Players in Collider")]
    private int playersInBox;

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

    public LevelData PortalInstanceData { get; private set; }

    public void Initialize(LevelData portalData)
    {
        if (portalData == null)
        {
            Debug.LogError("Portal Data Missing in Initialization", portalData);
            return;
        }

        PortalInstanceData = portalData;

        var position = transform.position;
        Instantiate(portalData.ObjectPrefab, new Vector3(position.x, position.y + 1, position.z), Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            PlayersInBox++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            PlayersInBox--;
        }
    }
}
