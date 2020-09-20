using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockTrigger : MonoBehaviour
{
    public Action<bool> setStatePlayerOnLockTrigger;
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.gameObject.tag.Equals("Player"))
    //     {
    //         setStatePlayerOnLockTrigger(true);
    //     }
    // }
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag.Equals("Player"))
    //     {
    //         setStatePlayerOnLockTrigger(false);
    //     }
    // }
}
