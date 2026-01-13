using UnityEngine;

public class MagicalCarryGem2D : MonoBehaviour
{
    [Header("References")]
    public Transform bird;
    public GemInventory inventory;

    private Transform currentGem;
    private GemSO lastGem; // 🔥 kritik
    private float fixedZ;

    void LateUpdate()
    {
        if (!inventory.HasGem())
        {
            RemoveGem();
            lastGem = null;
            return;
        }

        // 🔥 Gem değiştiyse
        if (inventory.equippedGem != lastGem)
        {
            ReplaceGem(inventory.equippedGem);
        }

        HandleOrbit(inventory.equippedGem);
    }

    void ReplaceGem(GemSO newGem)
    {
        RemoveGem();
        SpawnGem(newGem);
        lastGem = newGem;
    }

    void SpawnGem(GemSO gemData)
    {
        GameObject gemObj = Instantiate(gemData.gemPrefab);
        currentGem = gemObj.transform;
        fixedZ = currentGem.position.z;
    }

    void RemoveGem()
    {
        if (currentGem != null)
        {
            Destroy(currentGem.gameObject);
            currentGem = null;
        }
    }

    void HandleOrbit(GemSO gemData)
    {
        float angle = Time.time * gemData.orbitSpeed;

        Vector3 orbitOffset = new Vector3(
            Mathf.Cos(angle) * gemData.orbitRadius,
            0f,
            0f
        );

        float swingY = Mathf.Sin(Time.time * 2f) * 0.2f;

        Vector3 targetPos = bird.position + orbitOffset;
        targetPos.y = bird.position.y + gemData.orbitHeight + swingY;
        targetPos.z = fixedZ;

        currentGem.position = Vector3.Lerp(
            currentGem.position,
            targetPos,
            Time.deltaTime * 5f
        );
    }
}
