using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunderSound : MonoBehaviour
{
    [SerializeField] AudioSource soundThunder;
    public void PlayThunderSound()
    {
        soundThunder.Play();
    }
}
