using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    void Start()
    {
        Init();
    }

    void Update()
    {
        if (target == null)
            return;

        if (activation)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= nav.stoppingDistance)
            {
                StopMove();

                if (!IsPlayerFront())
                    RotateToPlayer();
                else
                {
                    Debug.Log("attack");
                    StartCoroutine(AttackCoroutine());
                }
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
