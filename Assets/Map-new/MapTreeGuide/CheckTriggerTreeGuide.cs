using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggerTreeGuide : MonoBehaviour
{
    public Action<bool> getTriggerState;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            getTriggerState(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            getTriggerState(false);
        }
    }
}
