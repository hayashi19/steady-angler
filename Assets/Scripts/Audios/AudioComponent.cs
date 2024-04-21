using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SfxAttribute
{
    public string sfxName;
    public AudioClip sfxClip;
}

public class AudioComponent : MonoBehaviour
{
    public MusicController musicController;

    public AudioSource sfxAudioSource;
    public List<SfxAttribute> sfxClips;

    public void PlaySFX(string sfxName, float volume = 1f, bool loop = false)
    {
        // get the audio from database
        SfxAttribute sfx = sfxClips.Find(attr => attr.sfxName == sfxName);

        // setup the audio
        sfxAudioSource.clip = sfx.sfxClip;
        sfxAudioSource.loop = loop;
        sfxAudioSource.volume = volume;
        sfxAudioSource.Play();
    }

    public void StopSFX()
    {
        if (sfxAudioSource.isPlaying) sfxAudioSource.Stop();
    }
}