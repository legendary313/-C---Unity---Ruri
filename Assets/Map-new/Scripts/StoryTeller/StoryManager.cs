using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    [SerializeField] GameObject Ra;
    [SerializeField] GameObject Chim;
    [SerializeField] GameObject Ruri;
    [SerializeField] GameObject Akiko;

    [SerializeField] GameObject Background;
    public SoundOnTellingStory sound;
    public int index = -1;
    Animator akikoAnimator, ruriAnimator, raAnimator, chimAnimator, backgroundAnimator;
    [Header("Loading")]
    [SerializeField] GameObject LoadingCanvas;
    [SerializeField] Image loadingFill;
    [SerializeField] TextMeshProUGUI loadingText;

    bool releaseTouch = true;
    private Queue<string> sentences;

    void Update() {
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if (releaseTouch && touch.phase == TouchPhase.Began){
                releaseTouch = false;
                DisplayNextSentence();
            }
            if (touch.phase == TouchPhase.Ended){
                releaseTouch = true;
            }
        }    
    }

    // Use this for initialization
    void Awake()
    {
        sentences = new Queue<string>();
        akikoAnimator = Akiko.GetComponent<Animator>();
        ruriAnimator = Ruri.GetComponent<Animator>();
        raAnimator = Ra.GetComponent<Animator>();
        chimAnimator = Chim.GetComponent<Animator>();
        backgroundAnimator = Background.GetComponent<Animator>();
        Screen.SetResolution(1280, 720, true);
    }

    public void StartDialogue(Story dialogue)
    {

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (index == -1)
        {
            akikoAnimator.SetTrigger("Appear");
            ruriAnimator.SetTrigger("Appear");
        }
        if (index > -1)
        {
            if (sound.isTelling(index) == true)
            {
                sound.StopTelling(index);
            }
            if (index == 1)
            {
                Ra.SetActive(true);
                raAnimator.SetTrigger("Appear");
                Chim.SetActive(true);
                chimAnimator.SetTrigger("Appear");
            }
            if (index == 2)
            {
                raAnimator.SetTrigger("Fade");
                akikoAnimator.SetTrigger("Fade");
                chimAnimator.SetTrigger("MoveRight");
                ruriAnimator.SetTrigger("MoveLeft");
            }
            if (index == 3)
            {
                Background.SetActive(true);
                backgroundAnimator.SetTrigger("Thunder");
            }
        }
        index++;
        Debug.Log("continue");
        if (sentences.Count == 0)
        {
            sound.StopTelling(index);
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        sound.PlayTelling(index);
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {

            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
            //yield return null;
        }
    }

    void EndDialogue()
    {
        sound.StopTelling(index-1);
        //Debug.Log(index);
        dialogueText.enabled = false;
        LoadingCanvas.SetActive(true);
        // StartCoroutine(LoadAsynchronouslyGameScene("Map1"));
        StartCoroutine(LoadAsynchronouslyGameScene("MainGame"));
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


}
