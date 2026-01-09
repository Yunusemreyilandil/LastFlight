using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TheHappyPrince.UI
{
    public class FadeTransition : MonoBehaviour
    {
        [Header("Fade Settings")]
        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private bool fadeInOnStart = true;

        private Coroutine currentFade;

        private void Start()
        {
            if (fadeInOnStart && fadeImage != null)
            {
                FadeIn();
            }
        }

        public void FadeIn(System.Action onComplete = null)
        {
            if (fadeImage == null) return;

            if (currentFade != null)
                StopCoroutine(currentFade);

            fadeImage.gameObject.SetActive(true);
            fadeImage.color = new Color(0, 0, 0, 1);
            
            currentFade = StartCoroutine(FadeCoroutine(0, () =>
            {
                fadeImage.gameObject.SetActive(false);
                onComplete?.Invoke();
            }));
        }

        public void FadeOut(System.Action onComplete = null)
        {
            if (fadeImage == null) return;

            if (currentFade != null)
                StopCoroutine(currentFade);

            fadeImage.gameObject.SetActive(true);
            fadeImage.color = new Color(0, 0, 0, 0);
            
            currentFade = StartCoroutine(FadeCoroutine(1, onComplete));
        }

        private IEnumerator FadeCoroutine(float targetAlpha, System.Action onComplete)
        {
            Color startColor = fadeImage.color;
            Color targetColor = new Color(0, 0, 0, targetAlpha);
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / fadeDuration);
                t = EaseInOutQuad(t);
                fadeImage.color = Color.Lerp(startColor, targetColor, t);
                yield return null;
            }

            fadeImage.color = targetColor;
            onComplete?.Invoke();
        }

        private float EaseInOutQuad(float t)
        {
            return t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;
        }

        private void OnDestroy()
        {
            if (currentFade != null)
                StopCoroutine(currentFade);
        }
    }
}
