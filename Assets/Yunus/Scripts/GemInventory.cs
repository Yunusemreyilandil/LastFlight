using UnityEngine;

public class GemInventory : MonoBehaviour
{
    [Header("Equipped Gem")]
    public GemSO equippedGem;

    [Header("Startup")]
    public bool clearOnStart = true;   // oyun baþýnda gem boþalsýn

    private void Start()
    {
        if (clearOnStart)
            equippedGem = null;
    }

    public void EquipGem(GemSO gem)
    {
        equippedGem = gem;
    }

    public void UnequipGem()
    {
        equippedGem = null;
    }

    public bool HasGem()
    {
        return equippedGem != null;
    }
}
