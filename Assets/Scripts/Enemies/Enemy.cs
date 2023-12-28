using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Globals.ColorsEnum color;
    
    private float _speed;
    private Lane _lane;

    public void Initialize(float speed, Lane lane)
    {
        _speed = speed;
        _lane = lane;
    }
    
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void Advance(Vector2 dest)
    {
        transform.position = dest;
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }
}
