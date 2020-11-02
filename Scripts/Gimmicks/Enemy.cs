using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public struct DamageInfo
{
    public int DamageValue;
    public int PlayerIndex;

}

public enum DamageType
{
    Shared,
    Unique
}

[SelectionBase]
public class Enemy : MonoBehaviour, IOnEventCallback
{
    public string ID;

    [Header("Stat")]
    public int MaxHP;
    public DamageType DamageType;

    public float Speed;

    public int KillScore;

    private IHealthStat healthStat;

    [Header("Destroy Settings")]
    public float WaitTimeUntilDestroy;

    private PhotonView photonView;
    private WaypointAgent agent;
    private Animator animator;

    private EnemyHUD enemyHUD;

    private bool IsDead = false;

    public GameObject DeathPrefab;



    void Awake()
    {
        agent = GetComponent<WaypointAgent>();
        animator = GetComponentInChildren<Animator>();
        photonView = GetComponent<PhotonView>();

        enemyHUD = GetComponentInChildren<EnemyHUD>();

        agent.MoveSpeed = Speed;

        if (DamageType == DamageType.Shared)
        {
            healthStat = new SharedHealthStat(MaxHP);
        }
        else
        {
            healthStat = new UniqueHealthStat(MaxHP, 2);
        }
    }


    public void AttemptSpawn(int waypointIndex)
    {
        photonView.RPC("OnSpawn_RPC", RpcTarget.All, waypointIndex);
    }

    [PunRPC]
    public void OnSpawn_RPC(int waypointIndex)
    {
        agent.InitialWaypoint = WaypointManager.Instance.GetWaypointByIndex(waypointIndex);
        agent.Play();
    }



    public IHealthStat GetStat()
    {
        return healthStat;
    }

    public DamageType GetDamageType()
    {
        return DamageType;
    }


    public void ApplyDamage(int playerIndex)
    {
        if (IsDead) return;
        photonView.RPC("OnDamageRPC", RpcTarget.All, playerIndex);
        
    }

    [PunRPC]
    private void OnDamageRPC(int playerIndex)
    {
        bool damageSuccess = healthStat.ApplyDamage(playerIndex);

        if (!damageSuccess)
        {
            
            return;
        }

        enemyHUD.UpdateStat();
        if (healthStat.IsZero())
        {
            OnDeath();
        }

    }

    IEnumerator DelayDestroy(float wait)
    {
        yield return new WaitForSeconds(wait);
        PhotonNetwork.Destroy(gameObject);
        EnemySpawnManager.Instance.NotifyDestroy(ID);
    }

    protected void OnDeath()
    {
        if (IsDead) return;

        IsDead = true;
        animator.applyRootMotion = true;
        animator.SetTrigger("Die");
        agent.enabled = false;

        ScoreManager.Instance.AppendScore(KillScore);
        var instance = Instantiate(DeathPrefab, transform.position, transform.rotation);

        if (photonView.IsMine)
        {
            StartCoroutine(DelayDestroy(WaitTimeUntilDestroy));
        }
    }

    [PunRPC]
    protected void OnDeathFeedback()
    { 
        
    }

    public void OnEvent(EventData photonEvent)
    {
        throw new System.NotImplementedException();
    }
}
