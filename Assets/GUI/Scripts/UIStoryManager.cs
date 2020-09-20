using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStoryManager : MonoBehaviour
{
    [SerializeField] DialogueManager DialogueManager;
    bool releaseTouch = true;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (releaseTouch && touch.phase == TouchPhase.Began)
            {
                releaseTouch = false;
                DialogueManager.DisplayNextSentence();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                releaseTouch = true;
            }
        }
    }
}
