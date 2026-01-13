using UnityEngine;


public class GemInventory : MonoBehaviour
{
    [Header("Equipped Gem")]
    public GemSO equippedGem;

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
