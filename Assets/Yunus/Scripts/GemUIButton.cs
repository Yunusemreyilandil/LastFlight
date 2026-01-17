using UnityEngine;
using UnityEngine.UI;

public class GemUIButton : MonoBehaviour
{
    [Header("This button's gem")]
    public GemSO gem;

    [Header("Refs (auto-find if empty)")]
    public GemInventory inventory;
    public StatueGemState statueState;

    [Header("UI")]
    public Button button;
    public Graphic[] visualsToHide; // Image/Text/TMP (Graphic) ne varsa

    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();

        // Auto-find
        if (inventory == null) inventory = FindFirstObjectByType<GemInventory>();
        if (statueState == null) statueState = FindFirstObjectByType<StatueGemState>();

        // 🔥 Eski onClick'leri temizle, sadece bunu bağla
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClickSelectGem);
        }

        Refresh();
    }

    public void OnClickSelectGem()
    {
        if (gem == null || inventory == null || statueState == null) return;

        // El doluyken seçme
        if (inventory.HasGem()) return;

        // Daha önce seçildiyse asla seçme
        if (!statueState.TryUse(gem))
        {
            Refresh();
            return;
        }

        // ✅ Seçim (artık bu gem kullanıldı sayıldı)
        inventory.EquipGem(gem);

        // UI güncelle (gizle + tıklanamaz)
        Refresh();
    }

    public void Refresh()
    {
        bool used = (statueState != null && statueState.IsUsed(gem));

        if (button != null) button.interactable = !used;

        if (visualsToHide != null)
        {
            foreach (var v in visualsToHide)
                if (v != null) v.enabled = !used;
        }

        // İstersen daha sert: komple yok et
        // gameObject.SetActive(!used);
    }
}
