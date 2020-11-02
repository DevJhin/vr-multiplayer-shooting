using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Photon.Pun;

public class ChestLock : MonoBehaviour
{
    public bool IsUnlocked;
    public string TargetTag;

    [SerializeField] Chest ownerChest;

    [SerializeField] TMP_Text stateText;
    [SerializeField] AudioSource openSFX;

    [SerializeField] PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    private void Lock()
    {
        if (!IsUnlocked) return;
        IsUnlocked = false;
        stateText.text = "Locked";
        stateText.outlineColor = Color.red;
        openSFX.Play();
    }

    [PunRPC]
    private void Unlock()
    {
        if (IsUnlocked) return;
        stateText.text = "Unlocked!";
        stateText.outlineColor = Color.blue;
        IsUnlocked = true;
        ownerChest.TryOpen();


    }

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(TargetTag))
        {
            photonView.RPC("Unlock", RpcTarget.All);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(TargetTag))
        {
            //photonView.RPC("Lock", RpcTarget.All);
        }
    }




}
