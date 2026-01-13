using UnityEngine;

public class StatueGemInteractor : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject inventoryUI;

    private bool playerInside = false;
    private bool inventoryOpen = false;

    void Update()
    {
        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryOpen = !inventoryOpen;
            inventoryUI.SetActive(inventoryOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        inventoryOpen = false;
        inventoryUI.SetActive(false);
    }
}
