using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSO : ScriptableObject
{
    public List<Enemy> enemies;
    public Vector2 duration;
    public Vector2 spawnRateRange;
    public Vector2 speedRange;

}
