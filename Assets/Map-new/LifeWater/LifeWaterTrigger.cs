using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeWaterTrigger : MonoBehaviour
{
    public Action<bool> getLifeWaterState;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            getLifeWaterState(true);
        }
    }
}
