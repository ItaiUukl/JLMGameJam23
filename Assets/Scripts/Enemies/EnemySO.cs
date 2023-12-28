using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] public Globals.ColorsEnum color;
    [SerializeField] public float speed;
}
