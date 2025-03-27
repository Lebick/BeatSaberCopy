using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audioSource;

    public void PlayMusic(AudioClip clip, float startTime)
    {
        audioSource.clip = clip;
        audioSource.Play();
        print(startTime);
        audioSource.time = startTime;
    }
}
