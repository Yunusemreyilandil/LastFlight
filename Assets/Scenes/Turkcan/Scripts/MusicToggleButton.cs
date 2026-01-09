using UnityEngine;
using UnityEngine.UI;

namespace TheHappyPrince.UI
{
    [RequireComponent(typeof(Button))]
    public class MusicToggleButton : MonoBehaviour
    {
        [Header("Button Images")]
        [SerializeField] private Image buttonImage;
        [SerializeField] private Sprite musicOnSprite;
        [SerializeField] private Sprite musicOffSprite;

        private Button button;
        private AudioManager audioManager;

        private void Awake()
        {
            button = GetComponent<Button>();
            
            if (buttonImage == null)
            {
                buttonImage = GetComponent<Image>();
            }
        }

        private void Start()
        {
            audioManager = AudioManager.Instance;
            
            if (button != null)
            {
                button.onClick.AddListener(OnButtonClicked);
            }

            UpdateButtonVisual();
        }

        private void OnButtonClicked()
        {
            if (audioManager != null)
            {
                audioManager.ToggleMusic();
                UpdateButtonVisual();
            }
        }

        private void UpdateButtonVisual()
        {
            if (buttonImage == null || audioManager == null) return;

            bool isMusicOn = audioManager.IsMusicEnabled();
            
            if (isMusicOn && musicOnSprite != null)
            {
                buttonImage.sprite = musicOnSprite;
            }
            else if (!isMusicOn && musicOffSprite != null)
            {
                buttonImage.sprite = musicOffSprite;
            }
        }

        private void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(OnButtonClicked);
            }
        }
    }
}
