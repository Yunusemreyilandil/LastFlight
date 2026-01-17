using UnityEngine;

public class GemSelectUI : MonoBehaviour
{
    public GemInventory inventory;

    // İstersen Inspector'dan da bağlayabilirsin; boşsa Instance kullanır
    public StatueGemState statueState;

    public void SelectGem(GemSO gem)
    {
        if (inventory == null) return;
        if (gem == null) return;

        if (statueState == null)
            statueState = StatueGemState.Instance;

        // El doluyken seçme
        if (inventory.HasGem()) return;

        // ✅ Daha önce seçildiyse tekrar seçme
        if (statueState != null && !statueState.TryUse(gem))
        {
            // UI'yı da güncelle (kullanılanlar görünmesin / tıklanamasın)
            RefreshAllGemButtons();
            Debug.Log("Bu gem daha önce seçildi, tekrar seçilemez!");
            return;
        }

        // ✅ Seçim
        inventory.EquipGem(gem);

        // UI'yı güncelle (seçilen artık görünmesin / tıklanamasın)
        RefreshAllGemButtons();
    }

    private void RefreshAllGemButtons()
    {
        // Sahnedeki tüm gem butonlarını bulup Refresh yaptır
        // (Butonlar GemUIButton kullanıyorsa otomatik gizlenir/tıklanamaz olur)
        var buttons = FindObjectsByType<GemUIButton>(FindObjectsSortMode.None);
        foreach (var b in buttons)
        {
            if (b != null) b.Refresh();
        }
    }
}
