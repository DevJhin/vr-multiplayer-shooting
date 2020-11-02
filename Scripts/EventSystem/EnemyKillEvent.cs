using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillEvent : IEvent
{
    public Enemy KillTarget;

    public float RequestTime;

    public EnemyKillEvent(Enemy target)
    {
        KillTarget = target;
        RequestTime = Time.time;


    }


}
