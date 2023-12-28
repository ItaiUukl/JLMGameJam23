using System.Collections;
using System.Collections.Generic;
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
        _score += enemy.scoreWorth;
        enemiesManager.OnEnemyDefeat(enemy);
    }

    void OnEnemyDamage() {
        _life -= 1;
    }
}
