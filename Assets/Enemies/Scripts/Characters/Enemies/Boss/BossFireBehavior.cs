using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBehavior : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyTargetWithTime(5f));
    }
    IEnumerator DestroyTargetWithTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // particle
            // StartCoroutine(DestroyBullet(0.2f));
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            // particle
            // StartCoroutine(DestroyBullet(0.2f));
            Destroy(gameObject);
        }
    }

}
