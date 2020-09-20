using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyWithTime(1f));
    }
    IEnumerator destroyWithTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
