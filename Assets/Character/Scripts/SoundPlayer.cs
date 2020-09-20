using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] AudioSource soundAttack;
    [SerializeField] AudioSource soundDash;
    [SerializeField] AudioSource soundHit;
    [SerializeField] AudioSource soundJump;
    [SerializeField] AudioSource soundMove;
    [SerializeField] AudioSource soundDie;
    [SerializeField] AudioSource soundWallSlide;
    [SerializeField] AudioSource soundChargeFlame;
    [SerializeField] AudioSource soundTakeItem;
    [SerializeField] AudioSource soundFirstCollectItem;
    [SerializeField] AudioSource soundDeathTheme;
    public void playSoundAttack()
    {
        soundAttack.Play();
    }
    public void playSoundDash()
    {
        soundDash.Play();
    }
    public void playSoundHit()
    {
        soundHit.Play();
    }
    public void playSoundJump()
    {
        soundJump.Play();
    }
    public void playSoundMove()
    {
        soundMove.Play();
    }
    public void playSoundDie()
    {
        soundDie.Play();
    }
    public void playSoundWallSlide()
    {
        soundWallSlide.Play();
    }
    public void playSoundChargeFlame()
    {
        soundChargeFlame.Play();
    }
    public void playSoundTakeItem()
    {
        soundTakeItem.Play();
    }
    public void playSoundFirstCollectItem()
    {
        soundFirstCollectItem.Play();
    }
    public void playerSoundDeathTheme()
    {
        soundDeathTheme.Play();
    }
}
