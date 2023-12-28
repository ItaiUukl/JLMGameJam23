using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Globals.ColorsEnum color;
    
    public float _speed;
    public Lane _lane;
    private int _currSegment = 0;
    private float _currAdvancement = 0f;

    public void Initialize(float speed, Lane lane)
    {
        _speed = speed;
        _lane = lane;
    }
    
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        if (transform.position == _lane.GetSegmentAt(_currSegment))
        {
            _currSegment++;
            _currAdvancement = 0;
        }

        _currAdvancement += _speed * Time.deltaTime;
        transform.position = _lane.AdvanceTo(_currSegment, _currAdvancement);
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }
}
