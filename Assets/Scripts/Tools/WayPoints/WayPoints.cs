using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour {

    [SerializeField]
    private List<Transform> waypoints;

    private int _waypointIndex;

    public Vector2 GetNextPoint() {
        if (_waypointIndex < waypoints.Count - 1)
            _waypointIndex++;
        else
            _waypointIndex = 0;

        return waypoints[_waypointIndex].position;
    }

    public Vector2 GetCurrentPoint()
    {
        return waypoints[_waypointIndex].position;
    }
}