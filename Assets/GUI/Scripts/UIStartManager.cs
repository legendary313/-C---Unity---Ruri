using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Video;

public class UIStartManager : MonoBehaviour
{
    public GameObject[] startBtn;
    public GameObject[] introductionBtn;
    public GameObject[] continueBtn;
    public GameObject[] exitBtn;
    public GameObject[] saveNotiYesBtn;
    public GameObject[] saveNotiNoBtn;
    public GameObject StartOrContinueOption;
    public GameObject continueButton;
    public GameObject objButton;
    public GameObject objLoading;
    public Image loadingFill;
    public TextMeshProUGUI loadingText;
    public AudioSource mainSound;
    public AudioSource introSound;
    public GameObject uiIntroduction;
    public VideoPlayer videoIntro;
    public VideoIntroBehavior videoIntroBehavior;
    [Header("Sound")]
    [SerializeField] SFXMusic soundClick;
    [SerializeField] SFXMusic soundEnter;

    void Start()
    {
        Time.timeScale = 1f;
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            continueButton.SetActive(true);
            PlayerPrefs.SetInt("SavedGame", 1);
        }
        else
        {
            continueButton.SetActive(false);
            PlayerPrefs.SetInt("SavedGame", 0);
        }
        Screen.SetResolution(1280, 720, true);
    }

    public void PointEnterStartBtn()
    {
        soundEnter.PlayMusic();
        startBtn[0].SetActive(true);
        startBtn[1].SetActive(false);
    }
    public void PointExitStartBtn()
    {
        startBtn[0].SetActive(false);
        startBtn[1].SetActive(false);
    }
    public void PointClickStartBtn()
    {
        soundClick.PlayMusic();
        startBtn[0].SetActive(true);
        startBtn[1].SetActive(true);

        if (PlayerPrefs.GetInt("SavedGame") == 1)
        {
            StartOrContinueOption.SetActive(true);
            objButton.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("PlayingMap", 1);
            objButton.SetActive(false);
            objLoading.SetActive(true);
            StartCoroutine(LoadAsynchronouslyGameScene("IntroStory"));
        }

    }

    IEnumerator LoadAsynchronouslyGameScene(string nameMap)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameMap);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            //Debug.Log(progress);
            loadingFill.fillAmount = progress;
            loadingText.text = "Loading " + Mathf.Round(progress * 100).ToString() + "%";
            yield return null;
        }
    }

    // Start new game
    public void YesButtonClick()
    {
        soundClick.PlayMusic();
        saveNotiYesBtn[0].SetActive(true);
        saveNotiYesBtn[1].SetActive(true);
        string path = Application.persistentDataPath + "/player.data";
        string map = Application.persistentDataPath + "/map.data";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        File.Delete(map);
        PlayerPrefs.SetInt("SavedGame", 0);
        PlayerPrefs.SetInt("PlayingMap", 1);
        StartOrContinueOption.SetActive(false);
        objButton.SetActive(false);
        objLoading.SetActive(true);
        StartCoroutine(LoadAsynchronouslyGameScene("IntroStory"));
    }

    // Return menu option
    public void NoButtonClick()
    {
        soundClick.PlayMusic();
        saveNotiNoBtn[0].SetActive(true);
        saveNotiNoBtn[1].SetActive(true);
        StartOrContinueOption.SetActive(false);
        objButton.SetActive(true);
    }

    public void PointEnterIntroductionBtn()
    {
        soundEnter.PlayMusic();
        introductionBtn[0].SetActive(true);
        introductionBtn[1].SetActive(false);
    }
    public void PointExitIntroductionBtn()
    {
        introductionBtn[0].SetActive(false);
        introductionBtn[1].SetActive(false);
    }
    public void PointClickIntroductionBtn()
    {
        soundClick.PlayMusic();
        introductionBtn[0].SetActive(true);
        introductionBtn[1].SetActive(true);
        ActiveUIIntroduction();
    }


    public void PointEnterContinueBtn()
    {
        soundEnter.PlayMusic();
        continueBtn[0].SetActive(true);
        continueBtn[1].SetActive(false);
    }
    public void PointExitContinueBtn()
    {
        continueBtn[0].SetActive(false);
        continueBtn[1].SetActive(false);
    }
    public void PointClickContinueBtn()
    {
        soundClick.PlayMusic();
        continueBtn[0].SetActive(true);
        continueBtn[1].SetActive(true);
        objButton.SetActive(false);
        objLoading.SetActive(true);
        //StartCoroutine(LoadAsynchronouslyGameScene("Map1"));
        StartCoroutine(LoadAsynchronouslyGameScene("MainGame"));
    }


    public void PointEnterExitBtn()
    {
        soundEnter.PlayMusic();
        exitBtn[0].SetActive(true);
        exitBtn[1].SetActive(false);
    }
    public void PointExitExitBtn()
    {
        exitBtn[0].SetActive(false);
        exitBtn[1].SetActive(false);
    }
    public void PointClickExitBtn()
    {
        soundClick.PlayMusic();
        exitBtn[0].SetActive(true);
        exitBtn[1].SetActive(true);
        Application.Quit();
    }
    public void PointEnterSaveNotiYesBtn()
    {
        soundEnter.PlayMusic();
        saveNotiYesBtn[0].SetActive(true);
        saveNotiYesBtn[1].SetActive(false);
    }
    public void PointExitSaveNotiYesBtn()
    {
        saveNotiYesBtn[0].SetActive(false);
        saveNotiYesBtn[1].SetActive(false);
    }
    public void PointEnterSaveNotiNoBtn()
    {
        soundEnter.PlayMusic();
        saveNotiNoBtn[0].SetActive(true);
        saveNotiNoBtn[1].SetActive(false);
    }
    public void PointExitSaveNotiNoBtn()
    {
        saveNotiNoBtn[0].SetActive(false);
        saveNotiNoBtn[1].SetActive(false);
    }

    public void ActiveUIIntroduction()
    {
        mainSound.Stop();
        objButton.SetActive(false);
        uiIntroduction.SetActive(true);
        videoIntro.Play();
        introSound.Play();
        StartCoroutine(waitFinishVideo());
    }
    IEnumerator waitFinishVideo()
    {
        while (!videoIntroBehavior.endVideo)
        {
            yield return null;
        }
        videoIntro.Stop();
        introSound.Stop();
        mainSound.Play();
        uiIntroduction.SetActive(false);
        objButton.SetActive(true);
        videoIntroBehavior.endVideo = false;
        //Debug.Log(videoIntroBehavior.endVideo);
    }
}
