using UnityEngine;

public class GemSelectUI : MonoBehaviour
{
    public GemInventory inventory;

    public void SelectGem(GemSO gem)
    {
        inventory.EquipGem(gem);
    }
}
