using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapBehavior : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Equals("PlayerMinimap"))
        {
            gameObject.SetActive(false);
        }
    }
}
