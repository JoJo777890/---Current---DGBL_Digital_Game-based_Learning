using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource SFXSource_Retry;

    [Header("---------- Audio Clip ----------")]
    public AudioClip correctAnimal;
    public AudioClip Retry;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFX_Retry(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
