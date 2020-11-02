using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
public class WaypointAgent : MonoBehaviour
{
    public Waypoint InitialWaypoint;

    [Header("Movement Settings")]
    public float MoveSpeed;
    public float SteerSpeed;

    public float ArriveDistance;

    [Header("Waypoint Selection")]
    public bool DoNotSelectPreviousWaypoint;


    private Waypoint fromWaypoint;
    private Waypoint toWaypoint;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private bool isPlaying;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        //Play();
    }

    void Update()
    {
        if (!isPlaying) return;
        if (!PhotonNetwork.IsMasterClient) return;

        if (HasArrived())
        {
            OnArrive();
        }
        Vector3 currentPos = transform.position;

        transform.position = Vector3.MoveTowards(currentPos, targetPosition, MoveSpeed * Time.deltaTime);

        Quaternion currentRot = transform.rotation;
        Vector3 toDirection = (targetPosition - currentPos).normalized;

        targetRotation = Quaternion.LookRotation(toDirection);
        if (Quaternion.Angle(currentRot, targetRotation) > 0.01f)
        { 
            transform.rotation = Quaternion.RotateTowards(currentRot,targetRotation, SteerSpeed * Time.deltaTime);
        }
    }

    bool HasArrived()
    {
        if (Vector3.Distance(transform.position, targetPosition) <= ArriveDistance)
        {
            return true;
        }

        return false;
    }

    void OnArrive()
    {
        Waypoint prevFromWaypoint = fromWaypoint;
        fromWaypoint = toWaypoint;

        SelectNextWaypoint(prevFromWaypoint);

        if (toWaypoint == null)
        {
            Stop();
        }
    }

    public void Play()
    {
        fromWaypoint = InitialWaypoint;
        if (fromWaypoint == null) return;

        SelectNextWaypoint(null);
        if (toWaypoint == null) return;

        transform.position = fromWaypoint.transform.position;
        transform.rotation = fromWaypoint.transform.rotation;
        
        isPlaying = true;
    }

    void Stop()
    {
        isPlaying = false;
    }

    [PunRPC]
    public void SetNextWaypoint(int waypointIndex)
    {
        try
        {
            var nextWaypoint = fromWaypoint.GetAdjacentAt(waypointIndex);
            toWaypoint = nextWaypoint;
            targetPosition = toWaypoint.transform.position;

        }
        catch (System.ArgumentOutOfRangeException e)
        {
            Debug.LogError("Out of range...: " + waypointIndex, gameObject);
        }
    }


    public void SelectNextWaypoint(Waypoint prevFromWaypoint)
    {
        int adjacentCount = fromWaypoint.GetAdjacentCount();

        int randIndex = Random.Range(0, adjacentCount);
        var tempNextWaypoint = fromWaypoint.GetAdjacentAt(randIndex);

        //Select again, if we selected waypoint that agent came from.
        if (DoNotSelectPreviousWaypoint && prevFromWaypoint == tempNextWaypoint)
        {
            randIndex = (randIndex + 1 + Random.Range(0, adjacentCount -1))%adjacentCount;
        }

        photonView.RPC("SetNextWaypoint", RpcTarget.MasterClient, randIndex);
    }

}
