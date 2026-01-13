using UnityEngine;

public class StatueGemInteractor : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject pressEPrompt;

    [Header("Animation Settings")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatAmount = 10f;

    private bool playerInside = false;
    private bool inventoryOpen = false;
    private Vector3 promptStartPos;

    void Start()
    {
        if (pressEPrompt != null)
        {
            promptStartPos = pressEPrompt.transform.localPosition;
            pressEPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInside && !inventoryOpen && pressEPrompt != null && pressEPrompt.activeSelf)
        {
            float newY = promptStartPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
            pressEPrompt.transform.localPosition = new Vector3(promptStartPos.x, newY, promptStartPos.z);
        }

        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryOpen = !inventoryOpen;
            inventoryUI.SetActive(inventoryOpen);
            
            if (pressEPrompt != null)
                pressEPrompt.SetActive(!inventoryOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        playerInside = true;
        
        if (pressEPrompt != null && !inventoryOpen)
            pressEPrompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        inventoryOpen = false;
        inventoryUI.SetActive(false);
        
        if (pressEPrompt != null)
            pressEPrompt.SetActive(false);
    }
}
