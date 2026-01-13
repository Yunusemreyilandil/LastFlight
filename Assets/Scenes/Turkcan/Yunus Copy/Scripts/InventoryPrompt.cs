using UnityEngine;
using UnityEngine.UI;

public class InventoryPrompt : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatAmount = 10f;

    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Vector2 startPosition;
    private float timeCounter = 0f;
    private bool isAnimating = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        startPosition = originalPosition;
    }

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (!isAnimating)
        {
            if (gameObject.activeSelf && Time.frameCount % 60 == 0)
                Debug.Log("InventoryPrompt Update: NOT animating but object is active!");
            return;
        }

        timeCounter += Time.deltaTime * floatSpeed;
        float newY = startPosition.y + Mathf.Sin(timeCounter) * floatAmount;
        rectTransform.anchoredPosition = new Vector2(startPosition.x, newY);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        
        // Pozisyonu orijinal pozisyona sıfırla
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = originalPosition;
            startPosition = originalPosition;
        }
        
        isAnimating = true;
        timeCounter = 0f;
        
        Debug.Log("InventoryPrompt.Show() called. OriginalPos: " + originalPosition + ", StartPos: " + startPosition + ", IsAnimating: " + isAnimating);
    }

    public void Hide()
    {
        isAnimating = false;
        gameObject.SetActive(false);
    }
}
