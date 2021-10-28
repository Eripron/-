using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController, IDamaged
{

    bool isIntimidate = false;
    bool isKnockDown = false;

    int moveCount = 0;
    float originSpeed;

    float accumulatedDamage = 0f;

    void Start()
    {
        Init();

        originSpeed = nav.speed;
    }

    void Update()
    {
        if (target == null || !isAlive)
            return;

        CheckDistanceToPlayer();
        SetSpeed();

        if (!isAttack && !isFar && !isIntimidate && !isKnockDown)
            RotateToPlayer();

        if (activation)
        {
            if (!isFar)
            {
                nav.speed = originSpeed;

                if (!IsPlayerFront())
                {
                    RotateToPlayer(true);
                }
                else
                {
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
            }
            else if (isFar)
            {
                if(moveCount >= 3000)
                {
                    moveCount = 0;
                    StartCoroutine(IntimidateAnimCoroutine());
                }
                else
                {
                    StartMove();
                    moveCount++;
                }
            }
        }
    }
    
    new void Init()
    {
        base.Init();
    }

    void SetSpeed()
    {
        if(moveCount >= 1000)
        {
            nav.speed = originSpeed * 1.5f;
        }
        else if(moveCount >= 2000)
        {
            nav.speed = originSpeed * 2f;
        }
        else
            nav.speed = originSpeed;
    }

    public override void Damaged(int _damage)
    {
        base.Damaged(_damage);
        accumulatedDamage += _damage;

        // 데미지 받은양이 일정 이상이라면 다운 모션 
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
    // 위협 관련
    IEnumerator IntimidateAnimCoroutine()
    {
        StopMove();

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
