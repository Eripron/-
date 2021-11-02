using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{
    BossHpUIManager bossHpUI;

    bool isBoss = false;


    public override int Hp 
    { 
        get => base.Hp;
        set 
        { 
            base.Hp = value;
        } 
    }


    void Start()
    {
        BossController boss = GetComponent<BossController>();
        if (boss != null)
        {
            isBoss = true;

            bossHpUI = BossHpUIManager.Instance;
            bossHpUI.OnInit(this);
        }

        InitStatus();
    }


    new void InitStatus()
    {
        base.InitStatus();
    }

    public override void OnDamaged(int damage)
    {
        base.OnDamaged(damage);

        if (isBoss)
        {
            bossHpUI.OnDamaged(damage);
        }
    }


}

