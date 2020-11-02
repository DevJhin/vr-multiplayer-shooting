using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
using Photon.Pun;

public class Scanner : MonoBehaviour
{
    [Header("Fire")]
    public LayerMask GunHitLayerMask;

    [Header("Gun Parts")]
    public Transform FireOrigin;
    public Transform Trigger;
    public GunHUD GunHUD;

    [Header("SFX")]
    public AudioSource FireSFX;
    public AudioSource ReloadSFX;

    [Header("VFX")]
    public ParticleSystem MuzzleVFX;
    public GameObject ImpactVFX;
    public GameObject EnergySphere;

    private PhotonView photonView;

    private readonly static float MaxDistance = 1.5f;

    private readonly static Vector3 Offset = new Vector3(0, 0.004f, 0.018f);

    private bool needReload;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        needReload = false;
        EnergySphere.transform.localScale = Vector3.zero;
    }


    public void AttemptFire()
    {
        if (!needReload)
        {
            photonView.RPC("FireFeedback", RpcTarget.All);

            var colliders = Physics.OverlapSphere(FireOrigin.position, MaxDistance, GunHitLayerMask);

            foreach (var collider in colliders)
            {
                int layer = collider.transform.gameObject.layer;
                if (layer == LayerMask.NameToLayer("Player"))
                {
                    var player = collider.transform.GetComponentInParent<HunterPlayer>();
                    Debug.Log("Discovered: " + player.PlayerID);

                    if (player == HunterGameMode.Instance.LocalHunterPlayer) continue;

                    player.Refill();
                }
            }
        }
    }

    [PunRPC]
    public void FireFeedback()
    {
        if(FireSFX!=null)
            FireSFX.Play();

        if (MuzzleVFX != null)
        {
        }
          //MuzzleVFX.Play();

    }

    [PunRPC]
    public void TriggerPullFeedback(float value)
    {
        EnergySphere.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, value);

    }

    [PunRPC]
    public void RefillFeedback()
    {
        ReloadSFX.Play();
    }

    [PunRPC]
    public void HitFeedback(Vector3 position, Vector3 normal)
    {
        //position += normal * 0.05f;
       // Quaternion particleRot = Quaternion.LookRotation(normal);
        //var instance = Instantiate(ImpactVFX, position, particleRot);

    }


}
