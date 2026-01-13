using UnityEngine;

public class InventoryPromptController : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private GameObject pressEPrompt;

    [Header("Animation Settings")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatAmount = 10f;

    private Vector3 promptStartPos;
    private StatueGemInteractor interactor;
    private bool wasInventoryOpen = false;

    void Start()
    {
        interactor = GetComponent<StatueGemInteractor>();
        
        if (pressEPrompt != null)
        {
            promptStartPos = pressEPrompt.transform.localPosition;
            pressEPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (interactor == null || pressEPrompt == null) return;

        bool playerInside = GetPlayerInsideStatus();
        bool inventoryOpen = GetInventoryOpenStatus();

        if (playerInside && !inventoryOpen)
        {
            if (!pressEPrompt.activeSelf)
                pressEPrompt.SetActive(true);

            float newY = promptStartPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
            pressEPrompt.transform.localPosition = new Vector3(promptStartPos.x, newY, promptStartPos.z);
        }
        else
        {
            if (pressEPrompt.activeSelf)
                pressEPrompt.SetActive(false);
        }

        if (inventoryOpen && !wasInventoryOpen)
        {
            pressEPrompt.SetActive(false);
        }

        wasInventoryOpen = inventoryOpen;
    }

    private bool GetPlayerInsideStatus()
    {
        var field = typeof(StatueGemInteractor).GetField("playerInside", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field != null)
            return (bool)field.GetValue(interactor);
        return false;
    }

    private bool GetInventoryOpenStatus()
    {
        var field = typeof(StatueGemInteractor).GetField("inventoryOpen", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field != null)
            return (bool)field.GetValue(interactor);
        return false;
    }
}
