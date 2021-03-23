using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Gui sounds
    public AudioClip hoverFx;
    public AudioClip clickFx;
    public AudioClip backFx;
    public AudioClip rotateFx;

    public AudioClip difficultySelectFx;

    public AudioSource audioSrc;


    public void HoverSound()
    {
        audioSrc.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        audioSrc.PlayOneShot(clickFx);
    }

    public void BackSound()
    {
        audioSrc.PlayOneShot(backFx);
 
    }

    public void DifficultySound()
    {
        audioSrc.PlayOneShot(difficultySelectFx);
 
    }

    public void RotationSound()
    {
        audioSrc.PlayOneShot(difficultySelectFx);
 
    }



}


