using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PauseMusic()
    {
        audioSource.Pause();
    }
    public void StopMusic()
    {
        audioSource.Pause();
    }
    public void PlayMusic()
    {
        audioSource.Play();
    }
    public void SettingMusic(float value)
    {
        audioSource.volume = value;
    }
}
