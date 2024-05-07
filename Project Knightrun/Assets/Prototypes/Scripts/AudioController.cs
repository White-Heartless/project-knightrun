using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] audioClips;
    [SerializeField]
    private AudioSource[] crunch;
    [SerializeField]
    private AudioSource[] swoosh;
    [SerializeField]
    private GameObject audios;


    public void MuteAll()
    {
        AudioListener.volume = 0f;
    }

    public void UnmuteAll()
    {
        AudioListener.volume = 1f;
    }

    public void MenuThemePlay()
    {
        audioClips[0].Stop();
        audioClips[4].Play();
    }

    public void MainThemePlay()
    {
        audioClips[4].Stop();
        audioClips[0].Play();
    }

    public void MainThemePitchDown()
    {
        audioClips[0].pitch = 0.2f;
    }

    public void MainThemePitchRestore()
    {
        audioClips[0].pitch = 0.9f;
    }

    public void AudioLosePlay()
    {
        audioClips[1].Play();
    }

    public void AudioRevivePlay()
    {
        audioClips[2].Play();
    }

    //used for premium currency and equip pieces (to udpate)
    public void AudioSpecialPlay()
    {
        audioClips[3].Play();
    }    

    public void AudioCashPlay()
    {
        audioClips[5].Play();
    }

    public void AudioTapPlay()
    {
        audioClips[6].Play();
    }

    public void AudioChestOpenPlay()
    {
        audioClips[7].Play();
    }
    
    public void AudioArmorStashPlay()
    {
        audioClips[8].Play();
    }
    
    public void AudioCrunchPlay()
    {
        crunch[Random.Range(0, crunch.Length)].Play();
    }

    public void AudioSwooshPlay()
    {
        swoosh[Random.Range(0, swoosh.Length)].Play();
    }
}
