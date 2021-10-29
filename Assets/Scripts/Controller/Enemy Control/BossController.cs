using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController, IDamaged
{

    bool isIntimidate = false;
    bool isKnockDown = false;
    bool isCheck = false;


    int moveCount = 0;
    float originSpeed;

    float accumulatedDamage = 0f;

    Coroutine CoAttack;

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

        Debug.Log(activation);

        if (!isAttack && !isFar && !isIntimidate && !isKnockDown)
            RotateToPlayer();

        if (activation)
        {
            if (!isFar)
            {
                moveCount = 0;
                nav.speed = originSpeed;

                RotateToPlayer(true);

                float percentage = Random.Range(0, 100);
                if (percentage < attackPercentage && !isAttack)
                {
                    CoAttack = StartCoroutine(AttackCoroutine());
                }
                else
                {
                    StartCoroutine(IntimidateAnimCoroutine());
                }
            }
            else if (isFar)
            {
                if (moveCount == 5000 && !isCheck)
                {
                    isCheck = true;
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
        if(moveCount >= 5000)
        {
            nav.speed = originSpeed * 4f;
        }
        else if(moveCount >= 2500)
        {
            nav.speed = originSpeed * 3f;
        }
        else if(moveCount >= 1500)
        {
            nav.speed = originSpeed * 2f;
        }
    }

    public override void Damaged(int _damage)
    {
        base.Damaged(_damage);
        accumulatedDamage += _damage;

        // 데미지 받은양이 일정 이상이라면 다운 모션 
        if(accumulatedDamage >= enemyStatus.MaxHp / 2 && enemyStatus.Hp > 0)
        {
            if (CoAttack != null)
            {
                StopCoroutine(CoAttack);
                isAttack = false;
            }

            StartCoroutine(KnockDownCoroutine());
            accumulatedDamage = 0f;
            return;
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
    // 넉다운
    IEnumerator KnockDownCoroutine()
    {
        activation = false;
        isKnockDown = true;

        anim.SetTrigger("OnKnockDown");
        yield return new WaitUntil(() => isKnockDown == false);

        activation = true;
    }
    public void OnEndKnockDown()
    {
        Debug.Log("call");
        isKnockDown = false;
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
