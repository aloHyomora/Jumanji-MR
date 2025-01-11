using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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


    public void PlatMusic(int index)
    {
        switch (index)
        {
            case 0:
                PlayAlaskaSound();
                break;
            case 1:
                PlayStartSound();
                break;
            case 2:
                PlayPairsSound();
                break;
            case 3:
                PlayStartSound();
                break;
            case 4:
                PlayCairoSound();
                break;
            case 5:
                PlayStartSound();
                break;
            case 6:
                PlayNewyorkSound();
                break;
            case 7:
                PlayStartSound();
                break;
            default:
                break;
        }
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
