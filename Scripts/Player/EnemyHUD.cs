using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyHUD : MonoBehaviour
{
    private Camera cam;
    
    [SerializeField]
    private Enemy owner;
    private IHealthStat healthStat;

    [SerializeField] HealthCellWidget[] healthWidgets;

    Queue<HealthCellWidget> HealthCellQueue;

    private void Start()
    {
        cam = HunterGameMode.Instance.LocalHunterPlayer.PlayerCamera;
        owner = GetComponentInParent<Enemy>();
        healthStat = owner.GetStat();


        foreach (var h in healthWidgets)
        {
            h.gameObject.SetActive(false);
        }

        HealthCellQueue = new Queue<HealthCellWidget>();
        if (owner.DamageType == DamageType.Shared)
        {
            SharedHealthStat stat = (SharedHealthStat)healthStat;
            for (int i = 0; i < stat.MaxValue; i++)
            {
                var healthWidget = healthWidgets[i];
                healthWidget.gameObject.SetActive(true);
                healthWidget.Init(-1);
                HealthCellQueue.Enqueue(healthWidget);
            }
        }
        else
        {
            UniqueHealthStat stat = (UniqueHealthStat)healthStat;
            foreach (var i in stat.Values)
            {
                var healthWidget = healthWidgets[i];
                healthWidget.gameObject.SetActive(true);
                healthWidget.Init(i);
                HealthCellQueue.Enqueue(healthWidget);
            }
        }
    }



    private void Update()
    {
        LookCamera();

    }

    public void UpdateStat()
    {
        var healthQueue = HealthCellQueue.Dequeue();
        healthQueue.SetFillAmount(0);
    }

    public void LookCamera()
    {
        Vector3 camPos = cam.transform.position;
        camPos.y = 0;
        Vector3 hudPos = transform.position;
        hudPos.y = 0;

        Vector3 forward = (hudPos - camPos).normalized;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }

    public void SetOwner(Enemy enemy)
    {
        owner = enemy;
    }

}
