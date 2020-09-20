using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeWaterBehavior : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] LifeWaterTrigger lifeWaterTrigger;
    [Header("Particle")]
    [SerializeField] ParticleSystem[] listParticleWater;
    [Header("UI")]
    [SerializeField] UIEventSystem uIEventSystem;
    [SerializeField] FloatingJoystick joystick;
    [Header("Sound")]
    [SerializeField] SFXMusic soundLoop;
    [SerializeField] SFXMusic[] soundStart;
    bool stateLifeWater = false;
    bool finishPlayParticle = true;
    private void Awake()
    {
        lifeWaterTrigger.getLifeWaterState = getLifeWaterState;
    }
    void getLifeWaterState(bool val)
    {
        if (val)
        {
            playWaterParticle();
        }
    }
    void playWaterParticle()
    {
        if (finishPlayParticle)
        {
            finishPlayParticle = false;
            uIEventSystem.InactiveAll();
            joystick.StopMovement();
            StartCoroutine(animWaterParticle());
        }
    }
    IEnumerator animWaterParticle()
    {
        for (int i = 0; i < listParticleWater.Length; i++)
        {
            listParticleWater[i].Play();
            soundStart[i].PlayMusic();
            if (i == listParticleWater.Length - 1)
            {
                soundLoop.PlayMusic();
            }
            Debug.Log(soundStart[i].name);
            yield return new WaitForSeconds(2.5f);
        }
        yield return new WaitForSeconds(1f);
        //finishPlayParticle = true;
        EndGameRuri();
    }
    void EndGameRuri()
    {
        uIEventSystem.EndGameRuri();
    }
}
