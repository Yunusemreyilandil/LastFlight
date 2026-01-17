using System.Collections.Generic;
using UnityEngine;

public class StatueGemState : MonoBehaviour
{
    public static StatueGemState Instance { get; private set; }

    private HashSet<GemSO> usedGems = new HashSet<GemSO>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool IsUsed(GemSO gem)
    {
        return gem != null && usedGems.Contains(gem);
    }

    // ✅ Gem daha önce kullanılmadıysa kullanır ve true döner.
    // ✅ Daha önce kullanıldıysa false döner.
    public bool TryUse(GemSO gem)
    {
        if (gem == null) return false;
        if (usedGems.Contains(gem)) return false;

        usedGems.Add(gem);
        return true;
    }
}
