using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class HunterGameMode : MonoSingleton<HunterGameMode>
{
    public HunterPlayer LocalHunterPlayer { get; set; }
    public GameResultData participant;

    public PlayerStart[] PlayerStarts;

    public TimeManager timeManager;
    public EnemySpawnManager spawnManager;

    public Camera PlayerCamera;


    void Awake()
    {
        

    }

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PlayerStarts = FindObjectsOfType<PlayerStart>();
            timeManager.OnTimerEnd += Finish;
            SpawnPlayer();
        }
    }

    public void Finish()
    {
        Application.Quit();
    }



    public int GetCurrentPlayerID()
    {
        return LocalHunterPlayer.PlayerID;
    }

    

    public void SpawnPlayer()
    {
        int playerID = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var playerStart = PlayerStarts[playerID];
        var playerInstance = PhotonNetwork.Instantiate("Prefabs/VRCharacter", playerStart.transform.position, playerStart.transform.rotation, 0);
        
        LocalHunterPlayer = playerInstance.GetComponent<HunterPlayer>();
        LocalHunterPlayer.PlayerID = playerID;
        LocalHunterPlayer.OnSpawn(playerID, ExperimentManager.Instance.GetAvatarID(), (int)ExperimentManager.Instance.settings.Gender);

    }

    public enum EventType
    { 
        Hit,
        Kill,
        Refill
    }


    public void LogEvent(EventType eventType, string senderInfo, string subjectInfo)
    {
        string message = "";
        double time = timeManager.GetPlayTime();

        switch (eventType)
        {
            case EventType.Hit:
                message = $"Hit Event::Sender{senderInfo}::Reciever{subjectInfo}";
                break;
            case EventType.Kill:
                message = $"Kill Event::Sender{senderInfo}::Reciever{subjectInfo}";
                break;
            case EventType.Refill:
                message = $"Refill Event::Sender{senderInfo}::Reciever{subjectInfo}";
                break;
        }


    }

    public void LogEvent_RPC(string message)
    { 
        
    }


}