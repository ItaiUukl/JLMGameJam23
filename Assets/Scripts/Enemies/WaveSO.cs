using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Wave", menuName = "Enemies Wave")]
public class WaveSO : ScriptableObject
{
    public List<Enemy> enemies;
    public Vector2 sizeRange;
    public Vector2 spawnRateRange;
    public Vector2 speedRange;

}
