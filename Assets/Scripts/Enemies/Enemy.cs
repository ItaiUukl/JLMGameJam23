using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] public Globals.ColorsEnum color;
    [SerializeField] public int scoreWorth;

    private float _speed;
    private Lane _lane;
    private int _id;
    private int _currSegment = 0;
    private float _currAdvancement = 0f;
    private Vector3 _startingPos;

    private Action<Enemy, DefensePod> _onPodCollision;

    
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    
    public void Initialize(int idInWave, float speed, Lane lane, Action<Enemy, DefensePod> onPodCollision)
    {
        Id = idInWave;
        _speed = speed;
        _lane = lane;
        _onPodCollision = onPodCollision;
        transform.position = _lane.spawnPoint.position;
    }
    
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
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
        if (other.gameObject.layer == Globals.PodLayer)
        {
            _onPodCollision(this, _lane.pod);
        }
    }
}
