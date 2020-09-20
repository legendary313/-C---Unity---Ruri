using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] GameObject explosion;
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.gameObject.tag.Equals("Ground"))
       {
           Instantiate(explosion, gameObject.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
           Destroy(gameObject);
       }
   }
}
