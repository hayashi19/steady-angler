using UnityEngine;

[System.Serializable]
public class MusicController
{
    public AudioSource musicAudioSource;
    public AudioClip[] musicClips;

    public void PlayMusic(int index)
    {
        this.musicAudioSource.PlayOneShot(this.musicClips[index]);
    }
}