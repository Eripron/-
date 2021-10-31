using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{
    bool isBoss = false;



    void Start()
    {
        InitStatus();

        BossController boss = GetComponent<BossController>();
        if (boss != null)
        {
            isBoss = true;

        }
    }

    new void InitStatus()
    {
        base.InitStatus();
    }

}
