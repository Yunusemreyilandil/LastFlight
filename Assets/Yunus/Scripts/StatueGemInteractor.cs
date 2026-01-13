using UnityEngine;

public class StatueInventoryTrigger : MonoBehaviour
{
    public GameObject gemUIRoot;

    private bool playerInside;

    void Start()
    {
        gemUIRoot.SetActive(false);
    }

    void Update()
    {
        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            gemUIRoot.SetActive(!gemUIRoot.activeSelf);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            gemUIRoot.SetActive(false);
        }
    }
}
