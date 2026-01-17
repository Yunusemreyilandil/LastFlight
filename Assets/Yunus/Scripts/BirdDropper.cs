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

    [Header("VFX")]
    public GameObject dropVfxPrefab;
    public float dropVfxDestroyAfter = 1.5f;

    [Tooltip("VFX kuşun üzerinde patlasın mı? Kapalıysa köyün merkezinde patlar.")]
    public bool vfxOnBird = false;

    [Tooltip("vfxOnBird açıksa bu nokta kullanılır; boşsa kuşun pozisyonu kullanılır.")]
    public Transform birdVfxPoint;

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
        if (inventory == null) { Debug.LogWarning("[BirdDropper] inventory yok"); return; }
        if (!inventory.HasGem()) return;
        if (inventory.equippedGem == null) return;

        // Sadece köy dropzone içindeyken
        if (currentZone == null)
        {
            Debug.Log("Gem sadece köyün alanındayken bırakılabilir!");
            return;
        }

        // Receiver bağlı mı?
        if (currentZone.receiver == null)
        {
            Debug.LogWarning("[BirdDropper] currentZone.receiver bağlı değil! DropZone.receiver alanını doldur.");
            return;
        }

        // Köy daha önce aldı mı?
        if (!currentZone.receiver.CanReceive())
        {
            Debug.Log("Bu köy zaten 1 kere gem aldı. Daha fazlasını kabul etmiyor.");
            return;
        }

        // ✅ Teslim
        currentZone.receiver.Receive(inventory.equippedGem);
        Debug.Log($"[DROP] Teslim edildi -> {currentZone.villageId}");

        // ✅ VFX
        SpawnDropVFX();

        // ✅ Envanter boşalt
        if (clearInventoryAfterDrop)
            inventory.UnequipGem();
    }

    void SpawnDropVFX()
    {
        if (dropVfxPrefab == null)
        {
            Debug.LogWarning("[VFX] dropVfxPrefab BOS! BirdDropper inspector'dan DropVFX prefab'ını bağla.");
            return;
        }

        Vector3 pos;

        if (vfxOnBird)
        {
            pos = birdVfxPoint != null ? birdVfxPoint.position : transform.position;
        }
        else
        {
            // Köyün merkezinde patlat
            pos = currentZone != null ? currentZone.transform.position : transform.position;
        }

        GameObject vfx = Instantiate(dropVfxPrefab, pos, Quaternion.identity);
        Debug.Log("[VFX] Spawn edildi: " + vfx.name);

        if (dropVfxDestroyAfter > 0f)
            Destroy(vfx, dropVfxDestroyAfter);
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
