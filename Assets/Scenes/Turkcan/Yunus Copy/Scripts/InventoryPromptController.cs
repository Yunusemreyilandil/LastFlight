using UnityEngine;

public class InventoryPromptController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private InventoryPrompt inventoryPrompt;

    private bool playerInside = false;
    private bool inventoryOpen = false;

    private void Start()
    {
        if (inventoryUI == null)
            Debug.LogError("InventoryPromptController: Inventory UI is not assigned!");
        
        if (inventoryPrompt == null)
            Debug.LogError("InventoryPromptController: Inventory Prompt is not assigned!");
    }

    void Update()
    {
        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryOpen = !inventoryOpen;
            
            if (inventoryUI != null)
                inventoryUI.SetActive(inventoryOpen);

            if (inventoryPrompt != null)
            {
                if (inventoryOpen)
                {
                    Debug.Log("Hiding prompt - Inventory opened");
                    inventoryPrompt.Hide();
                }
                else
                {
                    Debug.Log("Showing prompt - Inventory closed");
                    inventoryPrompt.Show();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        playerInside = true;
        Debug.Log("Player entered trigger area. InventoryOpen: " + inventoryOpen);

        if (inventoryPrompt != null)
        {
            if (!inventoryOpen)
            {
                Debug.Log("Showing prompt - Player entered");
                inventoryPrompt.Show();
            }
            else
            {
                Debug.Log("NOT showing prompt - Inventory is open");
            }
        }
        else
        {
            Debug.LogError("InventoryPrompt is NULL!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        inventoryOpen = false;
        Debug.Log("Player exited trigger area");
        
        if (inventoryUI != null)
            inventoryUI.SetActive(false);

        if (inventoryPrompt != null)
        {
            Debug.Log("Hiding prompt - Player exited");
            inventoryPrompt.Hide();
        }
    }
}
