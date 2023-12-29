using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _life = 3;
    private int _score = 0;

    [SerializeField] private EnemiesManager enemiesManager;

    void OnEnemyReachPod(Enemy enemy, DefensePod pod) {
        if (enemy.color == pod.color) {
            OnEnemyDefeat(enemy);
        }
        else {
            OnEnemyDamage();
        }
    }

    void OnEnemyDefeat(Enemy enemy) {
        Debug.Log("yay");
        _score += enemy.scoreWorth;
        enemiesManager.OnEnemyDefeat(enemy);
    }

    void OnEnemyDamage() {
        Debug.Log("ouch");
        _life -= 1;
    }

    private void Start()
    {
        enemiesManager.InitiateEnemies(OnEnemyReachPod);
    }
}
