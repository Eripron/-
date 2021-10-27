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
        if (target == null || !isAlive)
            return;

        CheckDistanceToPlayer();

        if (!isAttack && !isFar)
            RotateToPlayer();

        if (activation)
        {
            if (!isFar)
            {
                StopMove();

                float percentage = Random.Range(0, 100);
                if (percentage < attackPercentage && !isAttack)
                {
                    StartCoroutine(AttackCoroutine());
                }
                else
                {
                    StartCoroutine(IntimidateCoroutine());
                }
            }
            else if (isFar)
            {
                StartMove();
            }
        }
    }

    

    new void Init()
    {
        base.Init();
    }

    public override void Damaged(int _damage)
    {
        base.Damaged(_damage);
    }

    public override void Dead()
    {
        base.Dead();
        Debug.Log("dead");
    }

    IEnumerator IntimidateCoroutine()
    {
        yield return new WaitForSeconds(1f);
    }
}
