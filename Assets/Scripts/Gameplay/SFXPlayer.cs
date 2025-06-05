using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
