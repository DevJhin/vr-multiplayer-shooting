using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class HunterGameManager : MonoBehaviourPunCallbacks
{
    public static HunterGameManager Instance = null;

    public Text InfoText;
    public void Awake()
    {
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();

      //  CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
    }

    public void Start()
    {
        Hashtable properties = new Hashtable
        {
            {HunterGameSettings.PLAYER_LOADED_LEVEL, true}
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        //CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CheckEndOfGame();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        // if there was no countdown yet, the master client (this one) waits until everyone loaded the level and sets a timer start
        int startTimestamp;
        bool startTimeIsSet = CountdownTimer.TryGetStartTime(out startTimestamp);

        if (changedProps.ContainsKey(HunterGameSettings.PLAYER_LOADED_LEVEL))
        {
            if (CheckAllPlayerLoadedLevel())
            {
                if (!startTimeIsSet)
                {
                    CountdownTimer.SetStartTime();
                }
            }
            else
            {
                // not all players loaded yet. wait:
                Debug.Log("setting text waiting for players! ", this.InfoText);
                InfoText.text = "Waiting for other players...";
            }
        }

    }


    // called by OnCountdownTimerIsExpired() when the timer ended
    private void StartGame()
    {
        Debug.Log("StartGame!");

        if (PhotonNetwork.IsMasterClient)
        {
            //RobotSpawnManager.Start();
        }
    }

    public void InstantiatePlayer()
    {
        PhotonNetwork.Instantiate("Prefabs/Player", transform.position, transform.rotation, 0);
    }

    private bool CheckAllPlayerLoadedLevel()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;

            if (p.CustomProperties.TryGetValue(HunterGameSettings.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
            {
                if ((bool)playerLoadedLevel)
                {
                    continue;
                }
            }

            return false;
        }

        return true;
    }

    private void CheckEndOfGame()
    {
        
    }

}