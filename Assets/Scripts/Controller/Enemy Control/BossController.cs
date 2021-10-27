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

        if (!isAttack)
            RotateToPlayer();

        if (activation)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            // 거리에 따라 행동 나뉘어짐
            if (distance <= nav.stoppingDistance)
            {
                StopMove();

                int random = new System.Random().Next(0, 100);
                Debug.Log(random);

                // 확률에 따라 공격 or 대기 
                if (random < attackPercentage && !isAttack)
                {
                    if (IsPlayerFront())
                        StartCoroutine(AttackCoroutine());
                }
                else
                {
                }
            }
            else
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
