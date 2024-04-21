using UnityEngine;
using System.Collections.Generic;

// [System.Serializable]
// public class SfxAttribute
// {
//     public string sfxName;
//     public AudioClip sfxClip;
// }

[System.Serializable]
public class SfxController
{
    public AudioSource sfxAudioSource;
    public List<SfxAttribute> sfxClips;

    public void PlayOnShot(string sfxName, float volume = 1f)
    {
        SfxAttribute sfx = sfxClips.Find(attr => attr.sfxName == sfxName);

        // create new one to play
        GameObject audioObject = new("Throw SFX");

        // 
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.volume = volume;

        audioSource.PlayOneShot(sfx.sfxClip);

        // Destroy(audioObject, sfx.sfxClip.length);
    }

    // public void PlaySfx(AudioSource audioSource, string sfxName, float volume = 1f)
    // {
    //     SfxAttribute sfx = sfxClips.Find(attr => attr.sfxName == sfxName);
    //     if (sfx != null && sfx.sfxClip != null)
    //     {
    //         audioSource.volume = volume;
    //         audioSource.PlayOneShot(sfx.sfxClip);
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"SFX not found: {sfxName}");
    //     }
    // }

    // public SfxAttribute PlayLoopSfx(AudioSource audioSource, string sfxName, float volume = 1f)
    // {
    //     SfxAttribute sfx = sfxClips.Find(attr => attr.sfxName == sfxName);
    //     if (sfx != null && sfx.sfxClip != null)
    //     {
    //         audioSource.clip = sfx.sfxClip;
    //         audioSource.loop = true;
    //         audioSource.volume = volume;
    //         audioSource.Play();
    //         return sfx;
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"SFX not found: {sfxName}");
    //         return null;
    //     }
    // }

    // public void StopLoopSfx()
    // {
    //     if (sfxAudioSource.isPlaying)
    //         sfxAudioSource.Stop();
    // }
}
