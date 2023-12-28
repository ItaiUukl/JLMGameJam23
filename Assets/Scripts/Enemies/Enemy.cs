using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Globals.ColorsEnum _color;
    [SerializeField] public int scoreWorth;

    private float _speed;
    private Lane _lane;
    private int _currSegment = 0;
    private float _currAdvancement = 0f;
    private Vector3 _startingPos;

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
        _startingPos = transform.position;
    }
    
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        _startingPos = transform.position;
    }

    private void Update()
    {
        if (transform.position == _lane.GetSegmentAt(_currSegment+1))
        {
            _currSegment++;
            _currAdvancement = 0;
        }

        _currAdvancement += _speed * Time.deltaTime;
        transform.position = Vector3.Lerp(_lane.GetSegmentAt(_currSegment),_lane.GetSegmentAt(_currSegment+1), _currAdvancement);
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }
}
