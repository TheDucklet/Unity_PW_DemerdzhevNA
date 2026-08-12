using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Источники Звука")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Аудио Клипы")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip coinCollectSFX;
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip playerHitSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayCoinSound() => PlaySFX(coinCollectSFX);
    public void PlayButtonClickSound() => PlaySFX(buttonClickSFX);
    public void PlayHitSound() => PlaySFX(playerHitSFX);
}