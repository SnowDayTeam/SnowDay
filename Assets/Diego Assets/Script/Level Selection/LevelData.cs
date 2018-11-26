using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string SceneName;

    public GameObject ObjectPrefab;
}