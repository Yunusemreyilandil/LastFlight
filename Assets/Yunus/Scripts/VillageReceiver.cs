using UnityEngine;

public class VillageReceiver : MonoBehaviour
{
    [Header("State")]
    public bool isFilled = false;

    public bool CanReceive()
    {
        return !isFilled;
    }

    public void Receive(GemSO gem)
    {
        if (isFilled) return;

        isFilled = true;
        Debug.Log($"Köy gem aldý: {(gem != null ? gem.gemName : "null")}");

        // Sonra buraya: köy renklenme / VFX koyacaðýz
    }
}
