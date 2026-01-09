using UnityEngine;

namespace TheHappyPrince.UI
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Background Music")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip menuMusic;

        [Header("Sound Effects")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip buttonClickSound;
        [SerializeField] private AudioClip buttonHoverSound;

        [Header("Volume Settings")]
        [SerializeField] private float musicVolume = 0.5f;
        [SerializeField] private float sfxVolume = 1f;

        private bool isMusicEnabled = true;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadSettings();
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            if (musicSource != null && menuMusic != null)
            {
                musicSource.clip = menuMusic;
                musicSource.loop = true;
                musicSource.volume = musicVolume;
                
                if (isMusicEnabled)
                {
                    musicSource.Play();
                }
            }
        }

        public void ToggleMusic()
        {
            isMusicEnabled = !isMusicEnabled;
            
            if (musicSource != null)
            {
                if (isMusicEnabled)
                {
                    musicSource.Play();
                }
                else
                {
                    musicSource.Pause();
                }
            }

            SaveSettings();
        }

        public void SetMusicEnabled(bool enabled)
        {
            isMusicEnabled = enabled;
            
            if (musicSource != null)
            {
                if (isMusicEnabled)
                {
                    if (!musicSource.isPlaying)
                        musicSource.Play();
                }
                else
                {
                    musicSource.Pause();
                }
            }

            SaveSettings();
        }

        public bool IsMusicEnabled()
        {
            return isMusicEnabled;
        }

        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            if (musicSource != null)
            {
                musicSource.volume = musicVolume;
            }
            SaveSettings();
        }

        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
            if (sfxSource != null)
            {
                sfxSource.volume = sfxVolume;
            }
            SaveSettings();
        }

        public void PlayButtonClick()
        {
            if (sfxSource != null && buttonClickSound != null)
            {
                sfxSource.PlayOneShot(buttonClickSound, sfxVolume);
            }
        }

        public void PlayButtonHover()
        {
            if (sfxSource != null && buttonHoverSound != null)
            {
                sfxSource.PlayOneShot(buttonHoverSound, sfxVolume * 0.5f);
            }
        }

        private void LoadSettings()
        {
            isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }

        private void SaveSettings()
        {
            PlayerPrefs.SetInt("MusicEnabled", isMusicEnabled ? 1 : 0);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
            PlayerPrefs.Save();
        }
    }
}
