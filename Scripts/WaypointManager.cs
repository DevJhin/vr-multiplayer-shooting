using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoSingleton<WaypointManager>
{
    private Waypoint[] waypoints;

    void Awake()
    {
        waypoints = GetComponentsInChildren<Waypoint>();
    }

    public Waypoint GetRandomWaypoint()
    {
        int index = Random.Range(0, waypoints.Length);
        return waypoints[index];
    }

    public int GetRandomWaypointIndex()
    { 
        return Random.Range(0, waypoints.Length);
    }

    public Waypoint GetWaypointByIndex(int index)
    {
        return waypoints[index];
    }
}
