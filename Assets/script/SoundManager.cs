using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource runSource;

    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip runSound;
    public AudioClip treasureSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayRunSound()
    {
        if (runSource != null && !runSource.isPlaying && runSound != null)
        {
            runSource.clip = runSound;

            runSource.loop = true;

            runSource.Play();
        }
    }

    public void StopRunSound()
    {
        if (runSource != null && runSource.isPlaying)
        {
            runSource.Stop();
        }
    }
}