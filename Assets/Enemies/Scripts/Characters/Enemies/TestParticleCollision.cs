using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticleCollision : MonoBehaviour
{
    public float takedamge = 0f;
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag.Equals("Toxic"))
        {
            float damage = other.GetComponent<DamageEnemy>().damage;
            takedamge += damage;
            Debug.Log(takedamge);
        }
    }
}
