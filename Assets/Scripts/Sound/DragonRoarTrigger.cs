using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRoarTrigger : MonoBehaviour
{
    public SFXMusic soundRoar;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soundRoar.PlayMusic();
        }
    }
}
