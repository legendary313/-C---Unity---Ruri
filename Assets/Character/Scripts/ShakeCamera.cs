using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public void shakeCamera(float duration, float magnitude){
        StartCoroutine(Shake(duration, magnitude));
    }
    private IEnumerator Shake(float duration, float magnitude){
        Vector3 originalPos = transform.localPosition;
        float elapseTime = 0f;
        while (elapseTime < duration){
            float x = Random.Range(1f, -1f) * magnitude;
            float y = Random.Range(1f, -1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapseTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
