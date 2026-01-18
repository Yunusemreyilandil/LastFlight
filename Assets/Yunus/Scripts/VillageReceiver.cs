using UnityEngine;

public class VillageReceiver : MonoBehaviour
{
    [Header("State")]
    public bool isFilled = false;

    [Header("Hint (optional)")]
    public GameObject gemHint; // Köy üstündeki pulse gem ikonu (GemHint child)

    private void Awake()
    {
        // Eğer Inspector'dan bağlamadıysan otomatik bulmaya çalışsın
        if (gemHint == null)
        {
            Transform t = transform.Find("GemHint");
            if (t != null) gemHint = t.gameObject;
        }
    }

    public bool CanReceive()
    {
        return !isFilled;
    }

    public void Receive(GemSO gem)
    {
        if (isFilled) return;

        isFilled = true;
        Debug.Log($"Köy gem aldı: {(gem != null ? gem.gemName : "null")}");

        // ✅ Gem teslim edilince yönlendirme ikonunu kapat
        if (gemHint != null)
            gemHint.SetActive(false);

        // Sonra buraya: köy renklenme / VFX koyacağız
    }
}
