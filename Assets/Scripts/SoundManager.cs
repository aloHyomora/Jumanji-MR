using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip alaskaSound;
    public AudioClip cairoSound;
    public AudioClip islandSound;
    public AudioClip newyorkSound;
    public AudioClip parisSound;
    public AudioClip startSound;
    
    private void Awake()
    {
        audioSource.playOnAwake = false;        
    }

    public void PlayAlaskaSound()
    {
        PlaySound(alaskaSound);
    }

    public void PlayCairoSound()
    {
        PlaySound(cairoSound);
    }
    public void PlayIslandSound()
    {
        PlaySound(islandSound);
    }
    public void PlayNewyorkSound()
    {
        PlaySound(newyorkSound);
    }
    public void PlayPairsSound()
    {
        PlaySound(parisSound);
    }

    public void PlayStartSound()
    {
        PlaySound(startSound);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    
}
