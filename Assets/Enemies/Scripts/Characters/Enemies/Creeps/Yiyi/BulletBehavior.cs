using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // particle
            // StartCoroutine(DestroyBullet(0.2f));
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Ground"))
        {
            // particle
            // StartCoroutine(DestroyBullet(0.2f));
            Destroy(gameObject);
        }
    }
    // IEnumerator DestroyBullet(float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     Destroy(this.gameObject);
    // }
}
