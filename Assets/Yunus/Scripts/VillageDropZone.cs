using UnityEngine;

public class VillageDropZone : MonoBehaviour
{
    public string villageId = "Village";
    public VillageReceiver receiver;

    private void Reset()
    {
        receiver = GetComponentInParent<VillageReceiver>();
    }
}
