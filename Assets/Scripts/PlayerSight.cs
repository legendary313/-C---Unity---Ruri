using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    [Header("Light Object")]
    public Action<string> getValueLight;

    private void OnTriggerStay2D(Collider2D other)
    {
        getValueLight(other.gameObject.name);
    }
}
