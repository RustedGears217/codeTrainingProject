using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    [SerializeField] private List<Vector2> points;
    private int _currentPointIndex = 0;

    private void Awake()
    {
        var transforms = GetComponentsInChildren<Transform>(true);
        foreach (var t in transforms)
        {
            points.Add(t.position);
        }

        if (points.Count <= 0)
        {
            points.Add(new Vector2(0, 0));
        }
    }

    public Vector2 GetNextWaypointPosition()
    {
        _currentPointIndex++;
        if (_currentPointIndex >= points.Count) _currentPointIndex = 0;

        return points[_currentPointIndex];

    }


    void Update()
    {
        
    }
}
