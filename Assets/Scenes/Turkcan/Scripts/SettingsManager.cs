using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TheHappyPrince.UI
{
    public class SettingsManager : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private Slider voiceVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private TextMeshProUGUI voiceVolumeText;
        [SerializeField] private TextMeshProUGUI musicVolumeText;

        [Header("Back Button")]
        [SerializeField] private Button backButton;

        [Header("Menu Manager")]
        [SerializeField] private MainMenuManager menuManager;

        private void Start()
        {
            if (menuManager == null)
            {
                menuManager = FindObjectOfType<MainMenuManager>();
            }
            SetupUI();
            LoadSettings();
        }

        private void OnEnable()
        {
            if (voiceVolumeSlider != null && voiceVolumeText != null)
            {
                UpdateVolumeText(voiceVolumeText, voiceVolumeSlider.value);
            }

            if (musicVolumeSlider != null && musicVolumeText != null)
            {
                UpdateVolumeText(musicVolumeText, musicVolumeSlider.value);
            }
        }

        private void SetupUI()
        {
            if (voiceVolumeSlider != null)
            {
                voiceVolumeSlider.onValueChanged.AddListener(OnVoiceVolumeChanged);
                UpdateVolumeText(voiceVolumeText, voiceVolumeSlider.value);
            }

            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
                UpdateVolumeText(musicVolumeText, musicVolumeSlider.value);
            }

            if (backButton != null)
            {
                backButton.onClick.AddListener(OnBackButtonClicked);
            }
        }

        private void OnVoiceVolumeChanged(float value)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetSFXVolume(value);
            }
            UpdateVolumeText(voiceVolumeText, value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMusicVolume(value);
            }
            UpdateVolumeText(musicVolumeText, value);
        }

        private void UpdateVolumeText(TextMeshProUGUI text, float value)
        {
            if (text != null)
            {
                text.text = Mathf.RoundToInt(value * 100) + "%";
            }
        }

        private void LoadSettings()
        {
            if (AudioManager.Instance != null)
            {
                if (voiceVolumeSlider != null)
                    voiceVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

                if (musicVolumeSlider != null)
                    musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            }
        }

        private void OnBackButtonClicked()
        {
            Debug.Log("Back button clicked!");
            
            if (menuManager == null)
            {
                Debug.LogError("MenuManager is NULL! Finding it now...");
                menuManager = FindObjectOfType<MainMenuManager>();
            }
            
            if (menuManager != null)
            {
                Debug.Log("Calling OnBackToMainMenu...");
                menuManager.OnBackToMainMenu();
            }
            else
            {
                Debug.LogError("MenuManager still NULL! Cannot go back to main menu.");
            }
        }

        private void OnDestroy()
        {
            if (voiceVolumeSlider != null)
                voiceVolumeSlider.onValueChanged.RemoveListener(OnVoiceVolumeChanged);

            if (musicVolumeSlider != null)
                musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);

            if (backButton != null)
                backButton.onClick.RemoveListener(OnBackButtonClicked);
        }
    }
}
