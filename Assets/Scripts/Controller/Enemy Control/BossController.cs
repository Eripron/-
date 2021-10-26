using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController, IDamaged
{
    void Start()
    {
        Init();
    }

    void Update()
    {
        if (target == null)
            return;

        if(!isAttack)
            RotateToPlayer();

        if (activation)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= nav.stoppingDistance)
            {
                StopMove();

                if (!isAttack && IsPlayerFront())
                    StartCoroutine(AttackCoroutine());
            }
            else if (distance > nav.stoppingDistance)
            {
                StartMove();
            }
        }
    }


    new void Init()
    {
        base.Init();
    }
}
