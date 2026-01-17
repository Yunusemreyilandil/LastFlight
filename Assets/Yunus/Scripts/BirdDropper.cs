using UnityEngine;

public class BirdDropper : MonoBehaviour
{
    [Header("References")]
    public GemInventory inventory;

    [Header("Input")]
    public KeyCode dropKey = KeyCode.Space;

    [Header("Rules")]
    public bool clearInventoryAfterDrop = true;

    private VillageDropZone currentZone;

    private void Reset()
    {
        inventory = GetComponent<GemInventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(dropKey))
            TryDrop();
    }

    void TryDrop()
    {
        if (inventory == null) return;
        if (!inventory.HasGem()) return;
        if (inventory.equippedGem == null) return;

        // Sadece köy dropzone içindeyken bırakılabilir
        if (currentZone == null)
        {
            Debug.Log("Gem sadece köyün alanındayken bırakılabilir!");
            return;
        }

        // DropZone üstünde receiver bağlı olmalı
        if (currentZone.receiver == null)
        {
            Debug.LogWarning("Bu DropZone'da receiver bağlı değil! (VillageReceiver'ı DropZone.receiver alanına bağla)");
            return;
        }

        // Köy daha önce aldıysa kabul etme
        if (!currentZone.receiver.CanReceive())
        {
            Debug.Log("Bu köy zaten 1 kere gem aldı. Daha fazlasını kabul etmiyor.");
            return;
        }

        // ✅ Teslim et (görsel yok, sadece mantık)
        currentZone.receiver.Receive(inventory.equippedGem);
        Debug.Log($"Gem teslim edildi -> {currentZone.villageId}");

        if (clearInventoryAfterDrop)
            inventory.UnequipGem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var zone = other.GetComponent<VillageDropZone>();
        if (zone != null)
            currentZone = zone;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var zone = other.GetComponent<VillageDropZone>();
        if (zone != null && currentZone == zone)
            currentZone = null;
    }
}
