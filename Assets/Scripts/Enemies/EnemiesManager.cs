using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<SpawnerSO> waves;
    [SerializeField] private List<Lane> lanes;

    private Action<Enemy, DefensePod> _onEnemyPodCollision;

    private List<Enemy> _createdEnemies;
    private Dictionary<Globals.ColorsEnum, int> _enemiesByColor;

    public void OnEnemyDefeat(Enemy enemy) {
        
    }
}
