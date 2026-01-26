using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sfxSource;
    
    public void PlaySFX(AudioClip audio)
    {
        sfxSource.resource = audio;
        sfxSource.Play();
    }
}
