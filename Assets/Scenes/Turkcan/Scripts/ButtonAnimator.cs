using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

namespace TheHappyPrince.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Animation Settings")]
        [SerializeField] private float hoverScale = 1.1f;
        [SerializeField] private float pressScale = 0.95f;
        [SerializeField] private float animationDuration = 0.2f;

        [Header("Color Settings")]
        [SerializeField] private bool changeColorOnHover = true;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color hoverColor = new Color(1f, 0.9f, 0.7f);

        private Vector3 originalScale;
        private Image buttonImage;
        private Button button;
        private Coroutine currentAnimation;

        private void Awake()
        {
            originalScale = transform.localScale;
            buttonImage = GetComponent<Image>();
            button = GetComponent<Button>();

            if (buttonImage != null && changeColorOnHover)
            {
                normalColor = buttonImage.color;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (button != null && !button.interactable) return;

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayButtonHover();

            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            
            currentAnimation = StartCoroutine(AnimateScale(originalScale * hoverScale, animationDuration));

            if (buttonImage != null && changeColorOnHover)
            {
                StartCoroutine(AnimateColor(hoverColor, animationDuration));
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (button != null && !button.interactable) return;

            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            
            currentAnimation = StartCoroutine(AnimateScale(originalScale, animationDuration));

            if (buttonImage != null && changeColorOnHover)
            {
                StartCoroutine(AnimateColor(normalColor, animationDuration));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (button != null && !button.interactable) return;

            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            
            currentAnimation = StartCoroutine(AnimateScale(originalScale * pressScale, animationDuration * 0.5f));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (button != null && !button.interactable) return;

            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            
            currentAnimation = StartCoroutine(AnimateScale(originalScale * hoverScale, animationDuration * 0.5f));
        }

        private IEnumerator AnimateScale(Vector3 targetScale, float duration)
        {
            Vector3 startScale = transform.localScale;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                t = EaseOutBack(t);
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                yield return null;
            }

            transform.localScale = targetScale;
        }

        private IEnumerator AnimateColor(Color targetColor, float duration)
        {
            if (buttonImage == null) yield break;

            Color startColor = buttonImage.color;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                buttonImage.color = Color.Lerp(startColor, targetColor, t);
                yield return null;
            }

            buttonImage.color = targetColor;
        }

        private float EaseOutBack(float t)
        {
            float c1 = 1.70158f;
            float c3 = c1 + 1f;
            return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
        }

        private void OnDisable()
        {
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            
            transform.localScale = originalScale;
            
            if (buttonImage != null && changeColorOnHover)
            {
                buttonImage.color = normalColor;
            }
        }

        private void OnDestroy()
        {
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
        }
    }
}
