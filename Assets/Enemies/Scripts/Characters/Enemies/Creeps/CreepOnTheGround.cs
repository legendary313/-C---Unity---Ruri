using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class CreepOnTheGround : MonoBehaviour, IEnemyParameter
{
    //public GameObject prefabEnemy;
    public Action isDied;
    [SerializeField] private UIEnemyManager uiEnemyManager;
    public float creep_hp_total;
    public float creep_hp_cur;
    public float creep_damge;
    public bool enemyFollowPlayer = false;
    protected bool dead = false;
    protected bool isGrounded;
    protected bool hit = false;
    protected bool isWaiting = false;
    protected bool endAttack = true;
    protected Transform target;
    public float enemy_hp_total
    {
        get { return creep_hp_total; }
        set { enemy_hp_total = creep_hp_total; }
    }
    public float enemy_hp_cur
    {
        get { return creep_hp_cur; }
        set { enemy_hp_total = creep_hp_cur; }
    }
    public float enemy_damage
    {
        get { return creep_damge; }
        set { enemy_damage = creep_damge; }
    }
    protected Transform posEnemyCurrent;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        posEnemyCurrent = GetComponent<Transform>();
    }
    protected virtual void Update()
    {
        //FixHPEnemy();
        UpdateUIEnemy();
    }

    protected virtual void FixedUpdate()
    {

    }
    public void CheckHPEnemy()
    {
        if (enemy_hp_cur > 0)
        {
            dead = false;
        }
        if (enemy_hp_cur > enemy_hp_total)
        {
            creep_hp_cur = enemy_hp_total;
        }
        if (enemy_hp_cur <= 0)
        {
            creep_hp_cur = 0;
            dead = true;
            //KillEnemy();
        }
    }
    public void SpawnEnemy()
    {
        hit = false;
        dead = false;
        isWaiting = false;
        endAttack = true;
        target = null;
    }
    private void UpdateUIEnemy()
    {
        //Debug.Log(enemy_hp_cur);
        uiEnemyManager.hp_cur = enemy_hp_cur;
        uiEnemyManager.hp_total = enemy_hp_total;
    }
    public void KillEnemy()
    {
        //this.gameObject.SetActive(false);
        //PoolManager.Kill(prefabEnemy);
        Destroy(gameObject.transform.parent.gameObject);
        creep_hp_cur = enemy_hp_total;
        dead = false;
        isDied();
        Debug.Log("die");
    }
    public void RecieveDamage(float damage)
    {
        creep_hp_cur -= damage;
        CheckHPEnemy();
    }
    public void EnemySound()
    {

    }

}