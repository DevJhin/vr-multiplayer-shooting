using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Chest : MonoBehaviour
{
    public List<ChestLock> ChestLocks;
    public int RequestCount;

    public bool IsOpened;

    public AudioSource openSFX;

    private PhotonView photonView;

    [SerializeField] LinearMove doorMove;
    [SerializeField] GameObject container;
    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        container.SetActive(false);

    }

    private bool CheckOpen()
    {
        if (IsOpened) return false;

        foreach (var chestLock in ChestLocks)
        {
            if (!chestLock.IsUnlocked) return false;
        }

        return true;
    }

    public void TryOpen()
    {
        if (!CheckOpen()) return;

        OpenFeedback();
        //photonView.RPC("OpenFeedback",RpcTarget.All);

    }

    IEnumerator DelayDestroy(float wait)
    {
        yield return new WaitForSeconds(wait);
        PhotonNetwork.Destroy(gameObject);

    }


    private void OpenFeedback()
    {
        IsOpened = true;
        openSFX.Play();
        doorMove.Play();
        foreach (var chestLock in ChestLocks)
        {
            chestLock.gameObject.SetActive(false);
        }

        ScoreManager.Instance.AppendScore(10000);
        container.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(DelayDestroy(2.5f));
        }
    }
}
