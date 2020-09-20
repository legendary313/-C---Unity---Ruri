using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public Action<float> enemyDie;
    [SerializeField] List<EnemySpawner> listEnemy = new List<EnemySpawner>();
    
    // [SerializeField] GameObject enemySpawnerParent;
    void Awake(){

        // EnemySpawner[] listEnemySpawner = transform.gameObject.GetComponentsInChildren<EnemySpawner>();
        foreach(EnemySpawner enemySpawner in listEnemy){
            enemySpawner.getEnemyManager = getEnemyManager;
        }
    }

    public void oneEnemyDie()
    {
        enemyDie(1);
    }

    public EnemyManager getEnemyManager(){
        return this;
    }
    
}
