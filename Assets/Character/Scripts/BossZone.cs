using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    // [HideInInspector] public bool findPlayer = false;
    public Action startAttackPlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            startAttackPlayer();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
