using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform playerPoint;
    [SerializeField] private List<Transform> lanePath;
    [SerializeField] private int segmentsNum = 100;

    private List<Vector3> segments = new List<Vector3>();

    private void Start()
    {
        float totalLength = 0f;
        for (int i = 0; i < lanePath.Count - 1; i++)
        {
            totalLength += Vector3.Distance(lanePath[i].position, lanePath[i + 1].position);
        }

        float segmentSize = totalLength / segmentsNum;
        Vector3 currVec = lanePath[0].position;

        for (int i = 1; i < lanePath.Count; i++)
        {
            float leftover = 0f;
            Vector3 nextVec = lanePath[i].position;
            while (currVec != nextVec)
            {
                segments.Add(currVec);
                leftover = segmentSize - Vector3.Distance(currVec, nextVec);
                currVec = Vector3.MoveTowards(currVec, nextVec, segmentSize);
            }

            if (i < lanePath.Count - 1)
            {
                currVec = Vector3.MoveTowards(currVec, lanePath[i+1].position, leftover);
            }
            segments.Add(currVec);
        }
    }

    public Vector3 GetSegmentAt(int order)
    {
        if (order >= segments.Count) return Vector3.negativeInfinity;
        return segments[order];
    }

    public Vector3 AdvanceTo(int segment, float advancement)
    {
        if (segment >= segments.Count) return Vector3.negativeInfinity;
        return Vector3.Lerp(segments[0], segments[segment], advancement);
    }
}
