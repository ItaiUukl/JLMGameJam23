using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<SpawnerSO> waves;
    [SerializeField] private List<Lane> lanes;

    private List<Enemy> _createdEnemies;
    private Dictionary<Globals.ColorsEnum, int> _enemiesByColor;

    private void Update()
    {
        
    }
}
