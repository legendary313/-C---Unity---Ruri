using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Cinemachine;

public class GodTreeBehavior : MonoBehaviour
{
    [Header("UI Main")]
    public UIEventSystem uiMain;
    public GameObject questButton;
    public FloatingJoystick floatingJoystick;
    public BackgroundMusic mainTheme;
    public BackgroundMusic ivernTheme;
    int countmeet;
    public bool isStoryTelling = false;
    [Header("Camera")]
    public GameObject cam1;
    public GameObject cam2;
    public GameObject camNormal;
    public CinemachineBrain cinemachineBrain;
    public PlayableDirector timelineStart;
    public PlayableDirector timelineEnd;
    private void Start()
    {
        //loadMeetGodTreeData("");
    }
    private void Update()
    {
        //Debug.Log(timelineStart.duration + " - " + timelineStart.time + " - " + timelineStart.timeUpdateMode);
    }
    public void StartIvernTimeline()
    {
        cam1.SetActive(true);
        cam2.SetActive(true);
        cinemachineBrain.enabled = true;
        timelineStart.Play();
        StartCoroutine(waitStartIvernTimeline(timelineStart.duration));
        // Debug.Log(timelineStart.duration + " - " + timelineStart.time);
    }
    IEnumerator waitStartIvernTimeline(double time)
    {
        while (timelineStart.time < time - 0.1f)
        {
            yield return null;
        }
        cam1.SetActive(false);
    }
    public void EndIvernTimeline()
    {
        cam1.SetActive(true);
        cam2.SetActive(true);
        cinemachineBrain.enabled = true;
        timelineEnd.Play();
        StartCoroutine(waitEndIvernTimeline(timelineEnd.duration));
        // Debug.Log(timelineEnd.duration);
    }
    IEnumerator waitEndIvernTimeline(double time)
    {
        while (timelineEnd.time < time - 0.1f)
        {
            yield return null;
        }
        cam1.SetActive(false);
        cam2.SetActive(false);
        cinemachineBrain.enabled = false;
        camNormal.GetComponent<Camera>().orthographicSize = 5f;
        camNormal.transform.localPosition = Vector3.zero;
    }
    public void loadMeetGodTreeData(string data)
    {
        if (data.Equals(""))
        {
            countmeet = 0;
        }
        else
        {
            countmeet = Int32.Parse(data);
        }
        activeQuestButton();
        checkGodTree();
    }
    public void activeQuestButton()
    {
        if (countmeet == 0)
        {
            questButton.SetActive(false);
        }
        else
        {
            questButton.SetActive(true);
        }
    }

    public void checkGodTree()
    {
        if (countmeet == 0)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public string getMeetGodTreeData()
    {
        return countmeet.ToString();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag.Equals("Player") && countmeet == 0)
        {
            //floatingJoystick. = false;
            floatingJoystick.StopMovement();
            StartStory();

        }
    }
    private void StartStory()
    {
        Debug.Log("enter");
        StartIvernTimeline();
        BackgroundMusic[] bgMusic = FindObjectsOfType<BackgroundMusic>();
        mainTheme.PauseMusic();
        ivernTheme.PlayMusic();
        uiMain.ActiveUIStory();
        isStoryTelling = true;
        countmeet++;
    }
    public void EndStory()
    {
        EndIvernTimeline();
        ivernTheme.StopMusic();
        mainTheme.PlayMusic();
        GetComponent<Collider2D>().enabled = false;
        //isStoryTelling = false;
    }
}
