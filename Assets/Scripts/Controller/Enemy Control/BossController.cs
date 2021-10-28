using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController, IDamaged
{

    bool isIntimidate = false;
    bool isKnockDown = false;
    float accumulatedDamage = 0f;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (target == null || !isAlive)
            return;

        CheckDistanceToPlayer();

        if (!isAttack && !isFar && !isIntimidate && !isKnockDown)
            RotateToPlayer();

        if (activation)
        {
            if (!isFar)
            {
                StopMove();

                if (!IsPlayerFront())
                {
                    RotateToPlayer(true);
                }

                float percentage = Random.Range(0, 100);
                if (percentage < attackPercentage && !isAttack)
                {
                    StartCoroutine(AttackCoroutine());
                }
                else
                {
                    StartCoroutine(IntimidateAnimCoroutine());
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
        accumulatedDamage += _damage;

        // ������ �������� ���� �̻��̶�� �ٿ� ��� 
        if(accumulatedDamage >= enemyStatus.MaxHp / 2 && enemyStatus.Hp > 0)
        {
            Debug.Log("down");
            StartCoroutine(KnockDownCoroutine());
            accumulatedDamage = 0f;
        }

        if (enemyStatus.Hp <= 0)
        {
            Dead();
            return;
        }
    }

    public override void Dead()
    {
        base.Dead();
        
    }
    IEnumerator KnockDownCoroutine()
    {
        activation = false;
        isKnockDown = true;
        anim.SetTrigger("OnKnockDown");
        yield return new WaitUntil(() => isKnockDown == false);

        yield return new WaitForSeconds(2f);
        activation = true;
    }
    public void OnEndKnockDown()
    {
        isKnockDown = false;
        activation = true;
    }
    // ���� ����
    IEnumerator IntimidateAnimCoroutine()
    {
        activation = false;
        isIntimidate = true;

        anim.SetTrigger("OnIntimidate");
        yield return new WaitUntil(() => isIntimidate == false);

        activation = true;
    }
    public void OnEndIntimidate()
    {
        isIntimidate = false;
    }
}
