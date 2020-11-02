using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> Adjacents;

    public float Range;

    public bool IsOccupied;

    private static List<Waypoint> waypoints;

    public Vector3 Position;
    public Quaternion Rotation;
    
    void Awake()
    {
        Position = transform.position;
        Rotation = transform.rotation;
    }

    public int GetAdjacentCount()
    {
        return Adjacents.Count;
    }

    public Waypoint GetAdjacentAt(int index)
    {
        return Adjacents[index];
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 currentPos = transform.position;
        foreach (var waypoint in Adjacents)
        {
            Vector3 otherPos = waypoint.transform.position;

            GizmosUtils.DrawArrow2D(currentPos, otherPos);
        }
    }
}
