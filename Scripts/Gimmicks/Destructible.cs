using UnityEngine;
using Photon.Pun;

public class Destructible : MonoBehaviourPun
{
    [SerializeField]
    GameObject _explosionPrefab;

    [SerializeField]
    GameObject _scorePrefab;

    [SerializeField]
    private float _autoDestructTime;

    [SerializeField]
    SharedHealthStat lifeStat;


    [SerializeField]
    int _hitValue = 0;

    [SerializeField]
    bool _distructableByHit = true;

    private bool _isHit = false;
    private float _timeAlive;

    private void Start()
    {

    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        _timeAlive += Time.deltaTime;
        if (_timeAlive >= _autoDestructTime)
        {
            PhotonNetwork.Destroy(gameObject);
            enabled = false;
        }
    }


    public void MarkToDestroy()
    {
        // Send message to the Master client that we hit the target
        photonView.RPC("DestroyByHit", RpcTarget.AllViaServer, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    [PunRPC]
    void DestroyByHit(int hitPlayerId)
    {
        CalculateScore(hitPlayerId);

        if (_distructableByHit)
        {
            Instantiate(Resources.Load("Helper/TargetHit"), transform.position, transform.rotation);

            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        else
        {
            if (hitPlayerId == PhotonNetwork.LocalPlayer.ActorNumber)
                Instantiate(Resources.Load("Helper/BadTarget"), transform.position, transform.rotation);
        }
    }

    private void OnDestruction()
    { 
    
    }

    private void CalculateScore(int hitPlayerId)
    {
        if (hitPlayerId >= 0 && PhotonNetwork.IsMasterClient && !_isHit)
        {
            _isHit = _distructableByHit;

            //HunterGameMode.Instance.CountScoreToPlayer(hitPlayerId, _hitValue);
        }
    }

}
