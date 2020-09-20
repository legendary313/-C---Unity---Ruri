using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem Save1Particle;
    [SerializeField] ParticleSystem Save2Particle;
    [SerializeField] SFXMusic soundSave1;
    [SerializeField] SFXMusic soundSave2;
    [SerializeField] ParticleSystem fireEffect;
    [SerializeField] GameObject lightEffect;

    public void runSave1Particle(){
        Save1Particle.Play();
        soundSave2.PlayMusic();
    }

    public void runSave2Particle(){
        StartCoroutine(waitForSave1ParticleStop());
    }

    IEnumerator waitForSave1ParticleStop(){
        while (Save1Particle.isPlaying){
            yield return null;
        }
        Save2Particle.Play();
        soundSave1.PlayMusic();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightEffect.SetActive(true);
            fireEffect.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(waitStopFireForTime(5f));
        }
    }
    IEnumerator waitStopFireForTime(float time)
    {
        yield return new WaitForSeconds(time);
        fireEffect.Stop();
        lightEffect.SetActive(false);
    }
}
