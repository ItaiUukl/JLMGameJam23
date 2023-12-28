using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Globals.ColorsEnum _color;
    [SerializeField] public int scoreWorth;

    private float _speed;
    private Lane _lane;

    private Action<Enemy, DefensePod> _onPodCollision;

    public Globals.ColorsEnum color
    {
        get { return _color; }
        set { _color = value; }
    }

    public void Initialize(float speed, Lane lane, Action<Enemy, DefensePod> onPodCollision)
    {
        _speed = speed;
        _lane = lane;
        _onPodCollision = onPodCollision;
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
