using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    private float speed;
    private bool restoreTime;

    void Start(){
        restoreTime = false;
    }

    void Update(){
        if (restoreTime){
            if (Time.timeScale < 1){
                Time.timeScale += Time.deltaTime * speed;
            }else{
                Time.timeScale = 1;
                restoreTime = false;
            }
        }
    }

    public void Stoptime(float changeTime, float restoreSpeed, float delay){
        speed = restoreSpeed;
        if (delay > 0){
            StopCoroutine(StartTimeAgain(delay));
            StartCoroutine(StartTimeAgain(delay));
        }else{
            restoreTime = true;
        }
        Time.timeScale = changeTime;
    }

    IEnumerator StartTimeAgain(float delay){
        yield return new WaitForSecondsRealtime(delay);
        restoreTime = true;
    }
}
