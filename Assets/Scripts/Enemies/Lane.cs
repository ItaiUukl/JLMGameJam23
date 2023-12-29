using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public int TowerLife = 3;
    [SerializeField] public Globals.LanesEnum laneIndex;    
    [SerializeField] public Transform spawnPoint;    
    [SerializeField] private DefensePod _pod;
    [SerializeField] private List<Transform> lanePath;
    [SerializeField] private int segmentsNum = 100;

    private List<Vector3> segments = new List<Vector3>();
    
    public DefensePod pod
    {
        get { return _pod; }   // get method
        set { _pod = value; }  // set method
    }

    private void Start()
    {
        float totalLength = 0f;
        for (int i = 0; i < lanePath.Count - 1; i++)
        {
            totalLength += Vector3.Distance(lanePath[i].position, lanePath[i + 1].position);
        }

        float segmentSize = totalLength / segmentsNum;
        Vector3 currVec = lanePath[0].position;

        float leftover = 0f;
        for (int i = 1; i < lanePath.Count; i++)
        {
            Vector3 nextVec = lanePath[i].position;
            currVec = Vector3.MoveTowards(currVec, nextVec, leftover);
            while (currVec != nextVec)
            {
                segments.Add(currVec);
                leftover = segmentSize - Vector3.Distance(currVec, nextVec);
                currVec = Vector3.MoveTowards(currVec, nextVec, segmentSize);
            }
        }
    }

    public Vector3 GetSegmentAt(int order)
    {
        if (order >= segments.Count) return segments[^1];
        return segments[order];
    }

    public void OnTowerDamage() {
        if (TowerLife == 0) {
            Debug.Log(laneIndex + ": Tower is Gone!");
        }
        TowerLife -= 1;
        Debug.Log(laneIndex + ": Tower is Hit!");
    }
}
