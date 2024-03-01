using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public AudioSource soundEffectController;
    public AudioClip fishingEffect;

    public void playFishingEffect()
    {
        if (soundEffectController != null && fishingEffect != null)
        {
            soundEffectController.PlayOneShot(fishingEffect);
        }
    }
}
