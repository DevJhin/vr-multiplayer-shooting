using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
public class BoxSpawner : MonoBehaviour
{
    Waypoint[] waypoints;
    int boxCount = 0;
    void Start()
    {
        waypoints = GetComponentsInChildren<Waypoint>();
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(BoxSpawnRoutine());
        }

    }
    IEnumerator BoxSpawnRoutine()
    {
        yield return new WaitForSeconds(15f);

        while (true)
        {
            var waypoint = GetRandomWaypoint();
            waypoint.IsOccupied = true;

            var instance = PhotonNetwork.Instantiate("Prefabs/Chest", waypoint.Position, waypoint.Rotation);
            var chest = instance.GetComponent<Chest>();

            yield return new WaitForSeconds(90f);

        }
    }
    public Waypoint GetRandomWaypoint()
    {
        return waypoints[boxCount++];
    }
}
