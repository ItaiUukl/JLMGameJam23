using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<WaveSO> waves;
    [SerializeField] private List<Lane> lanes;

    private Action<Enemy> _onEnemyPodCollision;
    
    private Dictionary<int, Stack<int>> _disabledEnemies = new Dictionary<int, Stack<int>>();
    private Dictionary<int, List<Enemy>> _enemiesByIndex = new Dictionary<int, List<Enemy>>();

    private WaveSO _currWave;

    public void OnEnemyDefeat(Enemy enemy) {
        enemy.gameObject.SetActive(false);
        _disabledEnemies[enemy.Id].Push(_enemiesByIndex[enemy.Id].Count - 1);
    }

    public void InitiateEnemies(Action<Enemy> collisionAction)
    {
        _onEnemyPodCollision = collisionAction;
        StartCoroutine(EnemyWavesRoutine());
    }

    private Enemy CreateNewEnemy(int id)
    {
        Enemy newEnemy = Instantiate(_currWave.enemies[id]);
        newEnemy.gameObject.SetActive(false);
        _enemiesByIndex[id].Add(newEnemy);
        _disabledEnemies[id].Push(_enemiesByIndex[id].Count - 1);
        return newEnemy;
    }

    private Enemy GetAvailableEnemy(int id)
    {
        if (_disabledEnemies[id].Count == 0)
        {
            CreateNewEnemy(id);
        }

        Enemy enemy = _enemiesByIndex[id][_disabledEnemies[id].Pop()];
        enemy.gameObject.SetActive(true);

        return enemy;
    }
    
    private void SpawnForWave()
    {
        int enemyId = Random.Range(0, _currWave.enemies.Count);
        Enemy enemy = GetAvailableEnemy(enemyId);
        float speed = Random.Range(_currWave.speedRange.x, _currWave.speedRange.y);
        Lane lane = lanes[Random.Range(0, lanes.Count)];
        enemy.Initialize(enemyId, speed, lane, _onEnemyPodCollision);
    }

    private void ResetEnemiesPool()
    {
        foreach (var enemyList in _enemiesByIndex.Values)
        {
            foreach (Enemy enemy in enemyList)
            {
                Destroy(enemy.gameObject);
            }
        }
        _disabledEnemies = new Dictionary<int, Stack<int>>();
        _enemiesByIndex = new Dictionary<int, List<Enemy>>();
        
        for (int i = 0; i < _currWave.enemies.Count; i++)
        {
            _disabledEnemies[i] = new Stack<int>();
            _enemiesByIndex[i] = new List<Enemy>();
            CreateNewEnemy(i);
        }
    }

    private IEnumerator WaveRoutine()
    {
        int size = (int)Random.Range(_currWave.sizeRange.x, _currWave.sizeRange.y);
        for (int i = 0; i < size; i++)
        {
            SpawnForWave();
            yield return new WaitForSeconds(Random.Range(_currWave.spawnRateRange.x, _currWave.spawnRateRange.y));
        }
    }
    
    private IEnumerator EnemyWavesRoutine()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            _currWave = waves[i];
            ResetEnemiesPool();
            yield return StartCoroutine(WaveRoutine());
        }
    }
}
