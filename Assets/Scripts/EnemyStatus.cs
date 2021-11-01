using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{

    public override int Hp 
    { 
        get => base.Hp;
        set 
        { 
            base.Hp = value;
            BossHpUIManager.Instance.SetBossHpGage(hp);
        } 
    }


    void Start()
    {
        BossController boss = GetComponent<BossController>();
        if (boss != null)
        {
            BossHpUIManager.Instance.OnInit(this);
        }

        InitStatus();
    }


    new void InitStatus()
    {
        base.InitStatus();
    }

}
