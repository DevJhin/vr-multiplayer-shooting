using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Gun : MonoBehaviour
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
    public AudioSource EmptyFireSFX;
    [Header("VFX")]
    public ParticleSystem MuzzleVFX;
    public GameObject ImpactVFX;

    private int currentAmmo;
    private readonly static int MaxAmmo = 16;

    private readonly static int MaxDistance = 100;

    private readonly static Vector3 Offset = new Vector3(0, 0.004f, 0.018f);

    private bool needReload;

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentAmmo = MaxAmmo;
        needReload = false;
    }


    public void AttemptFire()
    {
        if (!needReload)
        {
            FireFeedback();
            photonView.RPC("FireFeedback", RpcTarget.Others);

            var ray = new Ray(FireOrigin.position, FireOrigin.forward);
            RaycastHit hitInfo;


            if (Physics.Raycast(ray, out hitInfo, MaxDistance, GunHitLayerMask))
            {
                HitFeedback(hitInfo.point, hitInfo.normal);
                photonView.RPC("HitFeedback", RpcTarget.Others, hitInfo.point, hitInfo.normal);

                int layer = hitInfo.transform.gameObject.layer;

                if (layer == LayerMask.NameToLayer("Enemy"))
                {
                    var damagable = hitInfo.transform.GetComponent<Damagable>();

                    int playerID = HunterGameMode.Instance.LocalHunterPlayer.PlayerID;
                    damagable.ApplyDamage(playerID);
                }

            }

            currentAmmo--;
            GunHUD.UpdateAmmoUI(currentAmmo, MaxAmmo);
            if (currentAmmo == 0)
            {
                needReload = true;
                GunHUD.ShowAmmoEmpty();
            }
        }
        else
        {
            EmptyFireSFX.Play();
        }
    }

    [PunRPC]
    public void FireFeedback()
    {
        FireSFX.Play();
        MuzzleVFX.Play();

    }

    [PunRPC]
    public void ImmuneFeedback()
    {
        //FireSFX.Play();
        //MuzzleVFX.Play();

    }

    [PunRPC]
    public void TriggerPullFeedback(float value)
    {
        Trigger.localPosition = Vector3.Lerp(Vector3.zero, Offset, value);
        
    }

    public void RefillFeedback()
    {
        ReloadSFX.Play();
        needReload = false;
        currentAmmo = MaxAmmo;
        GunHUD.UpdateAmmoUI(currentAmmo, MaxAmmo);
    }

    [PunRPC]
    public void HitFeedback(Vector3 position, Vector3 normal)
    {
        position += normal * 0.05f;
        Quaternion particleRot = Quaternion.LookRotation(normal);
        var instance = Instantiate(ImpactVFX, position, particleRot );

    }




}
