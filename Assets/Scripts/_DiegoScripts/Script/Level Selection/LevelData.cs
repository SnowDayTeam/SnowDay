using UnityEngine;

/// <summary>
/// Level Data
/// </summary>
/// <remarks>
/// Holds the data for each level.
/// </remarks>
[CreateAssetMenu(fileName = "Level", menuName = "Level/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    /// <summary>
    /// The scene that this level is linked to
    /// </summary>
    public string SceneName;

    /// <summary>
    /// the asset that should be instantiated to  represent the Gate
    /// </summary>
    public GameObject ObjectPrefab;
}