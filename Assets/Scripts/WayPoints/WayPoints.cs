using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour {

    [SerializeField]
    private List<Transform> waypoints;

    private int waypointIndex;

    public Vector2 GetNextPoint() {
        if (waypointIndex < waypoints.Count - 1)
            waypointIndex++;
        else
            waypointIndex = 0;

        return waypoints[waypointIndex].position;
    }

    public Vector2 GetCurrentPoint()
    {
        return waypoints[waypointIndex].position;
    }
}