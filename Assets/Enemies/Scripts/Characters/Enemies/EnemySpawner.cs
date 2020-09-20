using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    //public Pool enemyPool;
    [SerializeField] GameObject enemyPrefabs;
    GameObject enemy;// = null;
    [HideInInspector] public bool isDie = false;

    public delegate EnemyManager GetEnemyManager();
    public GetEnemyManager getEnemyManager;
    bool followPlayer = false;
    bool targetDetection = false;

    private void Start()
    {
        //enemy = enemyPool.Spawn(transform.position, transform.rotation, transform.localScale);
        //enemy.transform.SetParent(transform);
        enemy = Instantiate(enemyPrefabs, transform.position, transform.rotation, transform.parent);
        enemy.transform.SetParent(transform);
        enemy.GetComponentInChildren<CreepOnTheGround>().isDied = getEnemyManager().oneEnemyDie;
        enemy.SetActive(false);
    }
    void Update()
    {
        //Debug.Log(enemy);
        if (enemy != null && enemy.activeInHierarchy)
        {
            setActiveEnemy();
        }
        if (enemy == null && !isDie)
        {
            isDie = true;
            StartCoroutine(SpawnEnemy(10f));
            //Debug.Log("can spawn");
        }
    }
    IEnumerator SpawnEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        enemy = Instantiate(enemyPrefabs, transform.position, transform.rotation);
        enemy.transform.SetParent(transform);
        enemy.GetComponentInChildren<CreepOnTheGround>().isDied = getEnemyManager().oneEnemyDie;
        if (!enemy.activeSelf) enemy.SetActive(true);
        isDie = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            targetDetection = true;
            // enemy.SetActive(true);
            if (enemy != null) enemy.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            targetDetection = false;
            // if (enemy != null) enemy.SetActive(false);
        }
    }
    private void setActiveEnemy()
    {
        followPlayer = enemy.GetComponentInChildren<CreepOnTheGround>().enemyFollowPlayer;
        // if (targetDetection)
        // {
        //     enemy.SetActive(true);
        //     return;
        // }
        if (!targetDetection && !followPlayer)
        {
            Transform[] enemyTransform = enemy.GetComponentsInChildren<Transform>();
            foreach (Transform item in enemyTransform)
            {
                if (item.tag.Equals("Creep"))
                    item.position = transform.position;
            }
            enemy.SetActive(false);
            //Destroy(enemy);
            return;
        }

    }
    // private void OnCollisionEnter2D(Collision2D other)
    // {

    // }
    // private void OnCollisionExit2D(Collision2D other)
    // {

    // }
}
