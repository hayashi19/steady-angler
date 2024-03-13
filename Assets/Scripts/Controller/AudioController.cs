using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource soundEffectController;
    public AudioClip fishingEffect;

    public void PlayThrowBaitEffect()
    {
        if (soundEffectController != null && fishingEffect != null)
        {
            soundEffectController.PlayOneShot(fishingEffect);
        }
    }
}
