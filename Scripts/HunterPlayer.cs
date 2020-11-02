using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class HunterPlayer : MonoBehaviour
{
    [SerializeField] GameResultData ParticipantData;

    public float Speed;

    public int PlayerID;

    PhotonView photonView;

    HandController handController;
    public Camera PlayerCamera;
    
    PlayerAvatar playerAvatar;
    PlayerVoiceController voiceController;

    [SerializeField] Lifetime lifetime;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        handController = GetComponentInChildren<HandController>();
        voiceController = GetComponentInChildren<PlayerVoiceController>();

        PlayerCamera = GetComponentInChildren<Camera>();
        playerAvatar = GetComponentInChildren<PlayerAvatar>();

    }

    private void Start()
    {
        
    }


    public void OnSpawn(int playerID, string avatarID, int playerGender)
    {
        photonView.RPC("OnSpawn_RPC", RpcTarget.All, playerID, avatarID, playerGender);
    }


    [PunRPC]
    private void OnSpawn_RPC(int playerID, string avatarID, int playerGender)
    {
        this.PlayerID = playerID;

        playerAvatar.AvatarID = avatarID;
        playerAvatar.SpawnAvatarModel();
        voiceController.SetVoiceDistortion(ExperimentManager.Instance.settings.UseVoiceDistortion, (PlayerGender)playerGender);

        GetComponent<PlayerController>().OnPlayerIDUpdate(PlayerID);
    }

    public void Refill()
    {
        photonView.RPC("RefillFeedback", RpcTarget.All);
    }


    [PunRPC]
    private void RefillFeedback()
    {
        if (photonView.IsMine)
        { 
            handController.OnRefillRequest();
            photonView.RPC("ShowRefillEffect", RpcTarget.All);
        }


    }

    [PunRPC]
    private void ShowRefillEffect()
    {
        lifetime.gameObject.SetActive(true);
    }


}
