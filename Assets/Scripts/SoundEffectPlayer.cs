using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    // private bool isFirstSoundEffect = true;
    public AudioSource src;
    public AudioClip yaySoundEffect;

    public void PlayYaySoundEffect()
    {
        src.volume = 0.25f;
        // if (isFirstSoundEffect == true)
        // {
        //     src.clip = yaySoundEffect;
        //     src.Play();
        //
        //     isFirstSoundEffect = false;
        // }
        src.clip = yaySoundEffect;
        src.Play();
    }
}
