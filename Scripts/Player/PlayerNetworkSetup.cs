using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;


public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    PhotonView m_photonView;

    public GameObject VRRig;
    public GameObject PlayerHUD;

    [Header("Player Visualization")]
    [SerializeField]
    private Renderer bodyRenderer;

    private PlayerController playerController;
    private PlayerAvatarController avatarController;
    
    private HandController rightHandController;

    private void Awake()
    {
        playerController = GetComponentInChildren<PlayerController>();
        avatarController = GetComponentInChildren<PlayerAvatarController>();

        rightHandController = GetComponentInChildren<HandController>();

        m_photonView = GetComponent<PhotonView>();
    }

    void Start()
    {

        if (m_photonView.IsMine)
        {
            //Local Player
            SetupLocalPlayer();
        }
        else
        {
            //Remote player
            SetupRemotePlayer();
        }

    }

    public void SetupLocalPlayer()
    {
        PlayerHUD.SetActive(false);
    }

    public void SetupRemotePlayer()
    {
        VRRig.SetActive(false);

        playerController.enabled = false;
        avatarController.enabled = false;
        rightHandController.enabled = false;
    }

}
