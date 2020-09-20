using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    BackgroundMusic[] backgroundMusics;
    SFXMusic[] sfxMusics;
    float musicValue;
    float sfxValue;
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        //backgroundMusics = GameObject.FindObjectsOfType<BackgroundMusic>();
        LoadSoundData("1|1");
        //sfxMusics = GameObject.FindObjectsOfType<SFXMusic>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SaveSoundData());
        backgroundMusics = GameObject.FindObjectsOfType<BackgroundMusic>();
        sfxMusics = GameObject.FindObjectsOfType<SFXMusic>();
        SoundSetting();
    }
    public void LoadSoundData(string data)
    {
        //1|1
        if (data.Equals("") || data == null)
        {
            musicValue = 1f;
            sfxValue = 1f;
        }
        else
        {
            string[] splitData = data.Split('|');
            musicValue = float.Parse(splitData[0]);
            sfxValue = float.Parse(splitData[1]);
        }
        musicSlider.value = musicValue;
        sfxSlider.value = sfxValue;
    }
    public void SoundSetting()
    {
        musicValue = musicSlider.value;
        sfxValue = sfxSlider.value;
        foreach (BackgroundMusic other in backgroundMusics)
        {
            other.SettingMusic(musicValue);
        }
        foreach (SFXMusic other in sfxMusics)
        {
            other.SettingMusic(sfxValue / 1.5f);
        }
    }
    public string SaveSoundData()
    {
        musicValue = musicSlider.value;
        sfxValue = sfxSlider.value;
        string data = musicValue.ToString() + "|" + sfxValue.ToString();
        return data;
    }
}
