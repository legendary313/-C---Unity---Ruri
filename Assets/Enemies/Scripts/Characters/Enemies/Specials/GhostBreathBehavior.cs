using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GhostBreathBehavior : MonoBehaviour
{
    [Header("Toxic")]
    [SerializeField] ParticleSystem toxic;
    [Header("Time")]
    [Tooltip("Attack time")]
    [SerializeField] float attackTime;
    [Tooltip("Cool Down Time")]
    [SerializeField] float coolDownTime;
    [Header("Target Detection")]
    [SerializeField] TargetDetection targetDetection;
    Animator animator;
    bool isAttacking = false;
    bool setTrigger = false;
    protected readonly int atkAnimator = Animator.StringToHash("Attack");
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAttacking && !setTrigger && targetDetection.targetDetection)
        {
            setTrigger = true;
            animator.SetTrigger(atkAnimator);
        }
        if (!targetDetection.targetDetection)
        {
            toxic.Stop();
            animator.enabled = false;
        }
        else
        {
            animator.enabled = true;
        }
    }
    void Attack()
    {
        StartCoroutine(CoolDownAttack(attackTime, coolDownTime));
    }
    void StartAttack()
    {
        MainModule mainModule = toxic.main;
        mainModule.startLifetime = attackTime;
        toxic.Play();
    }
    IEnumerator CoolDownAttack(float atkTime, float cd)
    {
        isAttacking = true;
        StartAttack();
        yield return new WaitForSeconds(Mathf.Abs(atkTime + cd + 1f));
        setTrigger = false;
        isAttacking = false;
    }
}
