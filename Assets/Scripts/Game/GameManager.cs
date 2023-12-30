using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score = 0;

    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI gameOverText;

    void OnEnemyReachPod(Enemy enemy) {
        if (enemy.color == enemy.Lane.pod.color) {
            OnEnemyDefeat(enemy);
        }
        else {
            OnEnemyDamage(enemy);
        }
    }

    void OnEnemyDefeat(Enemy enemy) {
        _score += enemy.scoreWorth;
        UpdateUI();
        enemiesManager.OnEnemyDefeat(enemy);
    }

    void OnEnemyDamage(Enemy enemy) {
        var lane = enemy.Lane;
        var playersInLane = playerController.GetPlayersInLane(lane);

        if (playersInLane.Count > 0) {
            foreach (var player in playersInLane)
            {
                playerController.OnPlayerDamage(player);
            }
        }
        else {
            if (enemy.Lane.TowerLife == 0) {
                GameOver();
                return;
            }
            enemy.Lane.OnTowerDamage();
        }
        enemiesManager.OnEnemyDefeat(enemy);
    }

    void GameOver() {
        gameOverText.gameObject.SetActive(true);
    }

    void UpdateUI() {
        Debug.Log("SSSCCCOOREEE: " + _score);
    }

    private void Start()
    {
        enemiesManager.InitiateEnemies(OnEnemyReachPod);
        AudioManager.Instance.Play("soundtrack");
        
    }
}
