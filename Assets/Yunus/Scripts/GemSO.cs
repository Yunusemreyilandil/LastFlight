using UnityEngine;

[CreateAssetMenu(fileName = "NewGem", menuName = "Inventory/Gem")]
public class GemSO : ScriptableObject
{
    public string gemName;
    public Sprite icon;
    public GameObject gemPrefab;

    [Header("Orbit Overrides")]
    public float orbitRadius = 2f;
    public float orbitSpeed = 2f;
    public float orbitHeight = 1f;
}