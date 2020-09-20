using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI dialogueText;
    public GameObject GodTree;

    private Queue<string> sentences;
    [SerializeField] SFXMusic[] soundQuest;
    int totalSentence = 0;
    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            totalSentence++;
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (totalSentence - sentences.Count > 0)
            soundQuest[totalSentence - sentences.Count - 1].StopMusic();
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        //Debug.Log(totalSentence - sentences.Count);
        soundQuest[totalSentence - sentences.Count].PlayMusic();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        string[] splitSentence = sentence.Split('|');
        if (splitSentence.Length < 1) yield break;
        dialogueText.text += splitSentence[0];
        string[] words = splitSentence[1].Split(' ');
        // splitSentence[1].ToCharArray()
        foreach (string letter in words)
        {
            dialogueText.text += letter + " ";
            yield return new WaitForSeconds(0.2f);
            //yield return null;
        }
    }

    void EndDialogue()
    {
        FindObjectOfType<GodTreeBehavior>().uiMain.ActiveUIQuestPublic();
        FindObjectOfType<GodTreeBehavior>().activeQuestButton();
        // this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GodTree.GetComponent<GodTreeBehavior>().EndStory();
    }

}