using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicToPlay;

    private void Start()
    {
        if (musicSource == null || musicToPlay == null) return;

        musicSource.clip = musicToPlay;
        musicSource.loop = true;
        musicSource.Play();
    }
}
