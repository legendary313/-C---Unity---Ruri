using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIEventSystem : MonoBehaviour
{
    [Header("User Interface")]
    [SerializeField] GameObject uiPlayGame;
    [SerializeField] GameObject uiPause;
    [SerializeField] GameObject uiMap;
    [SerializeField] GameObject uiMenu;
    [SerializeField] GameObject uiQuest;
    [SerializeField] GameObject uiGameOver;
    [SerializeField] GameObject uiStory;
    [SerializeField] GameObject uiLoadScene;
    [SerializeField] GameObject blackImage;
    [SerializeField] GameObject uiChangeMap;
    [Header("UI Play Game")]
    [SerializeField] GameObject UIPlayGame_objectButtonTab;
    [SerializeField] RectTransform UIPlayGame_recTranButtonTab;
    CanvasGroup UIPlayGame_cgButtonTab;
    [Header("UI Pause Game")]
    [SerializeField] GameObject UIPause_saveView;
    [SerializeField] GameObject UIPause_settingView;
    [SerializeField] GameObject UIPause_helpView;
    [SerializeField] TextMeshProUGUI UIPause_TMP_Save;
    [SerializeField] TextMeshProUGUI UIPause_TMP_HelpID;
    [SerializeField] TextMeshProUGUI UIPause_TMP_HelpDetail;
    [Header("UI Menu")]
    [SerializeField] GameObject UIMenu_profileView;
    [SerializeField] GameObject UIMenu_achievementView;
    [SerializeField] GameObject UIMenu_abilityView;
    [SerializeField] Scrollbar UIMenu_Scrollbar;
    [Header("UI Map")]
    [SerializeField] GameObject cameraMinimap1;
    [SerializeField] GameObject cameraMinimap2;
    [SerializeField] RawImage MinimapImage;
    [SerializeField] Texture textureMap1;
    [SerializeField] Texture textureMap2;
    public Action setIconPlayerCenterMinimap;
    //public Action setPlayerIconPosition;
    [Header("UI Quest")]
    [SerializeField] TextMeshProUGUI[] UIQuest_TMP_QuestID;
    [SerializeField] TextMeshProUGUI[] UIQuest_TMP_QuestDetail;
    [HideInInspector] public bool chekcBlackBGFinish = false;
    [Header("Sound")]
    [SerializeField] SFXMusic soundClick;
    [Header("BlackImage")]
    [SerializeField] TextMeshProUGUI textEndGame;
    CanvasGroup imageBlackBG;
    CanvasGroup imageUIChangeMap;
    // Delegate
    public Action enableChangeMap;
    public Action activeUIMenu;
    void Start()
    {
        imageBlackBG = blackImage.GetComponent<CanvasGroup>();
        imageUIChangeMap = uiChangeMap.GetComponent<CanvasGroup>();
        //ActiveUIPlayGame();
        StartGame();
    }
    public void StartGame()
    {
        InactiveAll();
        blackImage.SetActive(true);
        imageBlackBG.alpha = 1f;
        chekcBlackBGFinish = false;
        StartCoroutine(waitBackgroundBlackToWhite(3f));
    }
    IEnumerator waitBackgroundBlackToWhite(float time)
    {
        while (imageBlackBG.alpha > 0)
        {
            chekcBlackBGFinish = false;
            imageBlackBG.alpha -= time / 10f * Time.deltaTime;
            yield return null;
        }
        chekcBlackBGFinish = true;
        InactiveAll();
        ActiveUIPlayGame();
    }
    public void ScreenWhiteToBlack()
    {
        InactiveAll();
        blackImage.SetActive(true);
        imageBlackBG.alpha = 0f;
        chekcBlackBGFinish = false;
        StartCoroutine(waitBackgroundWhiteToBlack(3f));
    }
    public void ActiveUIChangeMap(float time)
    {
        InactiveAll();
        uiChangeMap.SetActive(true);
        imageUIChangeMap.alpha = 0f;
        StartCoroutine(waitChangeMap(time));
    }
    IEnumerator waitChangeMap(float time)
    {
        while (imageUIChangeMap.alpha < 1)
        {
            imageUIChangeMap.alpha += Time.deltaTime / (time / 2);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        enableChangeMap();
        while (imageUIChangeMap.alpha > 0)
        {
            imageUIChangeMap.alpha -= Time.deltaTime / (time / 2);
            yield return null;
        }
        InactiveAll();
        ActiveUIPlayGame();
    }
    public void ActiveUIDie(float time)
    {
        InactiveAll();
        uiChangeMap.SetActive(true);
        imageUIChangeMap.alpha = 0f;
        StartCoroutine(waitScreenWhiteToBlack(time));
    }
    IEnumerator waitScreenWhiteToBlack(float time)
    {
        while (imageUIChangeMap.alpha < 1)
        {
            imageUIChangeMap.alpha += Time.deltaTime / time;
            yield return null;
        }
    }
    public void EndGameRuri()
    {
        InactiveAll();
        blackImage.SetActive(true);
        imageBlackBG.alpha = 0f;
        StartCoroutine(waitScreenEndGame(5f));
    }
    IEnumerator waitScreenEndGame(float time)
    {
        while (imageBlackBG.alpha < 1)
        {
            imageBlackBG.alpha += time / 10f * Time.deltaTime;
            yield return null;
        }
        textEndGame.enabled = true;
        StartCoroutine(waitTextEndGame());
    }
    IEnumerator waitTextEndGame()
    {
        string txt = "To be continue...";
        for (int i = 0; i < txt.Length; i++)
        {
            textEndGame.text += txt[i];
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("StartGame");
    }
    IEnumerator waitBackgroundWhiteToBlack(float time)
    {
        while (imageBlackBG.alpha < 1)
        {
            chekcBlackBGFinish = false;
            imageBlackBG.alpha += time / 10f * Time.deltaTime;
            yield return null;
        }
        InactiveAll();
        ActiveUIPlayGame();
        chekcBlackBGFinish = true;
    }
    public void UIPlayGame_ButtonPause()
    {
        soundClick.PlayMusic();
        PauseGame();
        ActiveUIPause();
    }
    public void UIPlayGame_ButtonMap()
    {
        soundClick.PlayMusic();
        ActiveUIMap();
    }
    public void UIPlayGame_ButtonMenu()
    {
        soundClick.PlayMusic();
        ActiveUIMenu();
    }
    public void UIPlayGame_ButtonQuest()
    {
        soundClick.PlayMusic();
        ActiveUIQuest();
        //UIQuest_ButtonQuest();
    }
    public void UIPlayGame_HideButtonTab()
    {
        soundClick.PlayMusic();
        UIPlayGame_cgButtonTab = UIPlayGame_objectButtonTab.GetComponent<CanvasGroup>();
        if (UIPlayGame_cgButtonTab.alpha == 1)
        {
            UIPlayGame_cgButtonTab.alpha = 0;
            UIPlayGame_recTranButtonTab.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (UIPlayGame_cgButtonTab.alpha == 0)
        {
            UIPlayGame_cgButtonTab.alpha = 1;
            UIPlayGame_recTranButtonTab.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }
    public void UIPause_ButtonSave()
    {
        soundClick.PlayMusic();
        UIPause_HideAllViewport();
        UIPause_saveView.SetActive(true);
        UIPause_TMP_Save.text = "Save game...";
    }
    public void UIPause_ButtonSetting()
    {
        soundClick.PlayMusic();
        UIPause_HideAllViewport();
        UIPause_settingView.SetActive(true);
    }
    public void UIPause_ButtonHelp()
    {
        soundClick.PlayMusic();
        UIPause_HideAllViewport();
        UIPause_helpView.SetActive(true);
        UIPause_TMP_HelpID.text = "Help...";
        UIPause_TMP_HelpDetail.text = "Detail...";
    }
    public void UIMenu_ButtonProfile()
    {
        soundClick.PlayMusic();
        UIMenu_Scrollbar.value = 1f;
    }
    public void UIMenu_ButtonAchievement()
    {
        soundClick.PlayMusic();
        UIMenu_Scrollbar.value = 0.5f;
    }
    public void UIMenu_ButtonAbility()
    {
        soundClick.PlayMusic();
        UIMenu_Scrollbar.value = 0f;
    }
    public void UIQuest_ButtonQuest()
    {
        soundClick.PlayMusic();
        int count = 0;
        foreach (TextMeshProUGUI tmpID in UIQuest_TMP_QuestID)
        {
            tmpID.text = "Quest: " + (count++).ToString();
        }
        count = 0;
        foreach (TextMeshProUGUI tmpDetail in UIQuest_TMP_QuestDetail)
        {
            tmpDetail.text = "Line: " + (count++).ToString();
        }
    }
    private void UIPause_HideAllViewport()
    {
        UIPause_saveView.SetActive(false);
        UIPause_settingView.SetActive(false);
        UIPause_helpView.SetActive(false);
    }
    public void BackUIPlayGame()
    {
        soundClick.PlayMusic();
        ResumeGame();
        ActiveUIPlayGame();
    }
    public void ExitGameButton()
    {
        //Thoat game
        soundClick.PlayMusic();
        BackToStartGame(3f);
    }
    public void BackToStartGame(float time)
    {
        InactiveAll();
        Time.timeScale = 1f;
        uiChangeMap.SetActive(true);
        imageUIChangeMap.alpha = 0f;
        StartCoroutine(waitBackToStartGame(time));
    }
    IEnumerator waitBackToStartGame(float time)
    {
        while (imageUIChangeMap.alpha < 1)
        {
            imageUIChangeMap.alpha += Time.deltaTime / time;
            yield return null;
        }
        SceneManager.LoadScene("StartGame");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ActiveUIPlayGame()
    {
        InactiveAll();
        uiPlayGame.SetActive(true);
    }
    private void ActiveUIPause()
    {
        soundClick.PlayMusic();
        InactiveAll();
        blackImage.SetActive(true);
        uiPause.SetActive(true);
    }
    private void ActiveUIMap()
    {
        soundClick.PlayMusic();
        InactiveAll();
        //setPlayerIconPosition();
        Time.timeScale = 0f;
        if (PlayerPrefs.GetInt("PlayingMap") == 1)
        {
            MinimapImage.texture = textureMap1;
            cameraMinimap1.SetActive(true);
            cameraMinimap2.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("PlayingMap") == 2)
        {
            MinimapImage.texture = textureMap2;
            cameraMinimap2.SetActive(true);
            cameraMinimap1.SetActive(false);
        }
        blackImage.SetActive(true);
        uiMap.SetActive(true);
        setIconPlayerCenterMinimap();

    }
    public void NonActiveUIMap()
    {
        soundClick.PlayMusic();
        BackUIPlayGame();
        Time.timeScale = 1f;
    }
    private void ActiveUIMenu()
    {
        soundClick.PlayMusic();
        InactiveAll();
        PauseGame();
        blackImage.SetActive(true);
        uiMenu.SetActive(true);
        activeUIMenu();
    }
    private void ActiveUIQuest()
    {
        soundClick.PlayMusic();
        InactiveAll();
        blackImage.SetActive(true);
        PauseGame();
        uiQuest.SetActive(true);
    }

    public void ActiveUIQuestPublic()
    {
        soundClick.PlayMusic();
        ActiveUIQuest();
    }
    public void ActiveUIStory()
    {
        soundClick.PlayMusic();
        InactiveAll();
        //blackImage.SetActive(true);
        uiStory.SetActive(true);
    }
    public void ActiveUILoadScene()
    {
        soundClick.PlayMusic();
        InactiveAll();
        //blackImage.SetActive(true);
        uiLoadScene.SetActive(true);
    }
    private void ActiveUIGameOver()
    {
        soundClick.PlayMusic();
        InactiveAll();
        blackImage.SetActive(true);
        uiGameOver.SetActive(true);
    }
    public void InactiveAll()
    {
        cameraMinimap1.SetActive(false);
        cameraMinimap2.SetActive(false);
        uiPlayGame.SetActive(false);
        uiPause.SetActive(false);
        uiMap.SetActive(false);
        uiMenu.SetActive(false);
        uiQuest.SetActive(false);
        uiGameOver.SetActive(false);
        uiStory.SetActive(false);
        uiLoadScene.SetActive(false);
        uiChangeMap.SetActive(false);
        blackImage.SetActive(false);
    }
}
