using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheHappyPrince.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("UI Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        [Header("Panels")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject settingsPanel;

        [Header("Scene Settings")]
        [SerializeField] private string gameSceneName = "GameScene";

        private void Start()
        {
            SetupButtons();
            
            if (settingsPanel != null)
                settingsPanel.SetActive(false);
        }

        private void SetupButtons()
        {
            if (playButton != null)
                playButton.onClick.AddListener(OnPlayButtonClicked);

            if (settingsButton != null)
                settingsButton.onClick.AddListener(OnSettingsButtonClicked);

            if (exitButton != null)
                exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        public void OnPlayButtonClicked()
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayButtonClick();
                
            Debug.Log("Play button clicked - Loading game scene...");
            
            if (!string.IsNullOrEmpty(gameSceneName))
            {
                SceneManager.LoadScene(gameSceneName);
            }
            else
            {
                Debug.LogWarning("Game scene name is not set!");
            }
        }

        public void OnSettingsButtonClicked()
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayButtonClick();
                
            Debug.Log("Settings button clicked");
            
            if (settingsPanel != null)
            {
                settingsPanel.SetActive(true);
                if (mainMenuPanel != null)
                    mainMenuPanel.SetActive(false);
            }
        }

        public void OnExitButtonClicked()
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayButtonClick();
                
            Debug.Log("Exit button clicked - Quitting application...");
            
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void OnBackToMainMenu()
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayButtonClick();
            
            if (settingsPanel != null)
                settingsPanel.SetActive(false);
                
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(true);
        }

        private void OnDestroy()
        {
            if (playButton != null)
                playButton.onClick.RemoveListener(OnPlayButtonClicked);

            if (settingsButton != null)
                settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);

            if (exitButton != null)
                exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }
    }
}
