using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class VRWorldManager : MonoBehaviourPunCallbacks
{

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string message = $"The new player '{newPlayer.NickName}({newPlayer.ActorNumber})' has entered to '{PhotonNetwork.CurrentRoom.Name}' (Player Count: {PhotonNetwork.CurrentRoom.PlayerCount})";
        Debug.Log(message);
    }

    public void LeaveRoom() 
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //PhotonNetwork.LoadLevel("LobbyScene");
    }   


}
