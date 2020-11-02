using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

[System.Serializable]
public struct TransformStreamData
{
    public Vector3 NetworkPosition;
    public Vector3 Direction;
    public Quaternion NetworkRotation;


    public void WriteToStream(PhotonStream stream)
    {
        stream.SendNext(NetworkPosition);
        stream.SendNext(Direction);
        stream.SendNext(NetworkRotation);
    }

    public void ReadFromStream(PhotonStream stream)
    {
        NetworkPosition = (Vector3)stream.ReceiveNext();
        Direction = (Vector3)stream.ReceiveNext();
        NetworkRotation = (Quaternion)stream.ReceiveNext();

    }


}


public struct TransformSyncData
{
    public Transform Transform;

    //Position Data
    public float Distance;
    public Vector3 Direction;

    public Vector3 NetworkPosition;
    public Vector3 StoredPosition;
    
    //Rotation Data
    public float Angle;

    public Quaternion NetworkRotation;
    public Quaternion StoredRotation;

    public bool IsFirstTake;

    public TransformSyncData(Transform transform)
    {
        this.Transform = transform;

        Distance = 0f;
        Direction = Vector3.zero;
        Angle = 0f;

        StoredPosition = Transform.localPosition;
        NetworkPosition = Vector3.zero;

        StoredRotation = Transform.localRotation;
        NetworkRotation = Quaternion.identity;

        IsFirstTake = false;
    }

    public void UpdateTransform(int SerializationRate)
    {
        float maxDistanceDelta = Distance * (2.0f / SerializationRate);
        Transform.localPosition = Vector3.MoveTowards(Transform.localPosition, NetworkPosition, maxDistanceDelta);

        float maxAngleDelta = Angle * (2.0f / SerializationRate);
        Transform.localRotation = Quaternion.RotateTowards(Transform.localRotation, NetworkRotation, maxAngleDelta);
    }

    public TransformStreamData WriteStreamData()
    {
        Direction = Transform.localPosition - StoredPosition;
        StoredPosition = Transform.localPosition;
        StoredRotation = Transform.localRotation;

        return new TransformStreamData()
        {
            Direction = Direction,
            NetworkPosition = Transform.localPosition,
            NetworkRotation = Transform.localRotation
        };
    }


    public void ReadStreamData(TransformStreamData streamData, float lag)
    {
        //Setup remote position.
        NetworkPosition = streamData.NetworkPosition;
        NetworkRotation = streamData.NetworkRotation;
        Direction = streamData.Direction;

        if (IsFirstTake)
        {
            Distance = 0;
            Angle = 0;
            Transform.localPosition = NetworkPosition;
            Transform.localRotation = NetworkRotation;

            IsFirstTake = false;

        }
        else
        {
            NetworkPosition += Direction * lag;
            Distance = Vector3.Distance(Transform.localPosition, NetworkPosition);
            Angle = Quaternion.Angle(Transform.localRotation, NetworkRotation);
        }
    }


}




public class PlayerAvatarSync : MonoBehaviour, IPunObservable
{
    public Transform BodyTransform;
    public Transform HMDTransform;
    public Transform LeftHandTransform;
    public Transform RightHandTransform;

    TransformSyncData BodySync;
    TransformSyncData HMDSync;
    TransformSyncData LeftHandSync;
    TransformSyncData RightHandSync;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        BodySync = new TransformSyncData(BodyTransform);
        HMDSync = new TransformSyncData(HMDTransform);
        LeftHandSync = new TransformSyncData(LeftHandTransform);
        RightHandSync = new TransformSyncData(RightHandTransform);

    }

    void Start()
    {
    }

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            BodySync.UpdateTransform(PhotonNetwork.SerializationRate);
            HMDSync.UpdateTransform(PhotonNetwork.SerializationRate);
            LeftHandSync.UpdateTransform(PhotonNetwork.SerializationRate);
            RightHandSync.UpdateTransform(PhotonNetwork.SerializationRate);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            BodySync.WriteStreamData().WriteToStream(stream);
            HMDSync.WriteStreamData().WriteToStream(stream);
            LeftHandSync.WriteStreamData().WriteToStream(stream);
            RightHandSync.WriteStreamData().WriteToStream(stream);
        }
        else
        {
            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));

            var streamData = new TransformStreamData();

            streamData.ReadFromStream(stream);
            BodySync.ReadStreamData(streamData, lag);

            streamData.ReadFromStream(stream);
            HMDSync.ReadStreamData(streamData, lag);

            streamData.ReadFromStream(stream);
            LeftHandSync.ReadStreamData(streamData, lag);

            streamData.ReadFromStream(stream);
            RightHandSync.ReadStreamData(streamData, lag);
        }
    }

}
