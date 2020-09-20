using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnTellingStory : MonoBehaviour
{
    [SerializeField] AudioSource[] soundsTellingStory;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isTelling(int index)
    {
        if(soundsTellingStory[index].isPlaying == true)
        {
            return true;
        }
        return false;
        
    }

    public void PlayTelling(int index)
    {
        soundsTellingStory[index].Play();
    }

    public void StopTelling(int index)
    {
        soundsTellingStory[index].Stop();
    }
    public void PauseTelling(int index)
    {
        soundsTellingStory[index].Pause();
    }
}
