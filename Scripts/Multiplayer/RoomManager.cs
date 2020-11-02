using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Photon_Hashtable = ExitGames.Client.Photon.Hashtable;


public class RoomManager : MonoBehaviourPunCallbacks
{
    public const string MAP_NAME_KEY = "map";

    public const string MAP_NAME_GAME = "GameScene";

    private string mapName;

    RoomUIManager roomUIManager;


    // Start is called before the first frame update
    void Start()
    {
        roomUIManager = FindObjectOfType<RoomUIManager>();

        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinLobby();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }


    #region UI Callback Methods

    public void EnterRandomRoom()
    {
        mapName = MAP_NAME_GAME;
        var expectedRoomProperties = new Photon_Hashtable()
        {
            {MAP_NAME_KEY, mapName }
        };

        PhotonNetwork.JoinRandomRoom(expectedRoomProperties, 0);
    }

    #endregion


    #region Photion Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError(message);

        CreateAndJoinRoom();   
    }

    #endregion

    public override void OnCreatedRoom()
    {
        Debug.Log($"A room is create with the name '{PhotonNetwork.CurrentRoom.Name}'.");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        string message = $"The local player: {PhotonNetwork.NickName} joined to {PhotonNetwork.CurrentRoom.Name} (Player Count: {PhotonNetwork.CurrentRoom.PlayerCount})";
        Debug.Log(message);

        Room currentRoom = PhotonNetwork.CurrentRoom;

        object mapNameObject;
        if (currentRoom.CustomProperties.TryGetValue(MAP_NAME_KEY, out mapNameObject))
        {
            string joinedMapName = mapNameObject as string;
            Debug.Log("Joined room with the map: " + joinedMapName);

            if (joinedMapName == MAP_NAME_GAME)
            {
                PhotonNetwork.LoadLevel(MAP_NAME_GAME);
            }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to Lobby");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string message = $"The new player '{newPlayer.NickName}' has entered to '{PhotonNetwork.CurrentRoom.Name}' (Player Count: {PhotonNetwork.CurrentRoom.PlayerCount})";
        Debug.Log(message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomInfoList)
    {
        if (roomInfoList.Count == 0)
        {
            roomUIManager.SetOccupancyText(0, 20);
            return;       
        }

        foreach (var roomInfo in roomInfoList)
        {
            Debug.Log(roomInfo.Name);
            if (roomInfo.Name.Contains(MAP_NAME_GAME))
            {
                roomUIManager.SetOccupancyText(roomInfo.PlayerCount, roomInfo.MaxPlayers);
            }
        }
    }


    private void CreateAndJoinRoom()
    {
        string randomRoomName = $"Room_{mapName}_{Random.Range(0,10)}";
        string[] roomPropsInLobby = {MAP_NAME_KEY};

        //Room Properties Hashtable
        var customRoomProperties = new Photon_Hashtable() { 
            {MAP_NAME_KEY, mapName}
        };

        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 3,
            CustomRoomPropertiesForLobby = roomPropsInLobby,
            CustomRoomProperties =  customRoomProperties
            
        };

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
}
