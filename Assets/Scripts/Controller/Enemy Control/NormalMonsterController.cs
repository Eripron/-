using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterController : EnemyController
{
    bool isGoToBack = false;
    int layer;

    bool isGenerated = false;

    Coroutine coWaitMoment;

    void Start()
    {
        Init();
        StartCoroutine(CreatedWaitTimeCoroutine());
    }

    IEnumerator CreatedWaitTimeCoroutine()
    {
        yield return new WaitForSeconds(2f);
        isGenerated = true;
    }

    void Update()
    {
        if (!isGenerated)
            return;

        if (target == null || !isAlive)
            return;

        CheckDistanceToPlayer();

        // 공격중이 아니고 가깝다면 플레이어를 향해 회전 
        if (!isAttack && !isFar && !isDamaged)
            RotateToPlayer();

        if (activation)
        {
            if (!isFar)
            {
                StopMove();

                float percentage = Random.Range(0, 100);
                if (percentage < attackPercentage && !isAttack)
                {
                    if(IsPlayerFront())
                        StartCoroutine(AttackCoroutine());
                }
                else if(IsPlayerFront())
                {
                    if (coWaitMoment != null)
                        StopCoroutine(coWaitMoment);

                    coWaitMoment = StartCoroutine(WaitSomeCoroutine());
                }
            }
            else if (isFar)
            {
                StartMove();
            }
        }
        
    }

    void FixedUpdate()
    {
        if (!isAlive || isDamaged)
            return;

        if(isWait)
            transform.position = Vector3.MoveTowards(transform.position, waitDir, Time.deltaTime * 1.8f);
    }

    new void Init()
    {
        base.Init();
    }

    public override void Damaged(int _damage)
    {
        base.Damaged(_damage);

        // normal only
        activation = false;
        isWait = false;

        StopMove();
        anim.Rebind();

        if (enemyStatus.Hp <= 0)
        {
            Dead();
            return;
        }

        StartCoroutine(DamagedAnimCoroutine());
    }
    public void OnEndDamagedAnim()
    {
        isDamaged = false;
    }
    public override void Dead()
    {
        base.Dead();

        EnemyGenerator generator = GetComponentInParent<EnemyGenerator>();
        if(generator != null)
        {
            generator.OnChangeEnemyCount(this);
        }

        if (coWaitMoment != null)
            StopCoroutine(coWaitMoment);

        StartCoroutine(DisappearCoroutine());
    }

    IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    // damaged coroutine
    IEnumerator DamagedAnimCoroutine()
    {
        isDamaged = true;
        isAttack = false;

        anim.SetTrigger("OnDamaged");

        yield return new WaitUntil(() => isDamaged == false);
        yield return new WaitForSeconds(0.7f);

        activation = true;
    }

    // wait coroutine
    protected IEnumerator WaitSomeCoroutine()
    {
        activation = false;
        isGoToBack = true;
        yield return new WaitForSeconds(1f);

        float random = Random.Range(0, 7);
        switch (random)
        {
            case 0:
            case 1:
                waitDir = (-transform.right);
                break;
            case 2:
            case 3:
                waitDir = transform.right;
                break;
            case 4:
            case 5:
            case 6:
                waitDir = (-transform.forward);
                break;
        }
        waitDir = transform.position + waitDir * 1.5f;
        waitDir.y = 0;

        isWait = true;

        anim.SetTrigger("OnBackMove");

        yield return new WaitUntil(() => isGoToBack == false);
        isWait = false;

        waitDir = Vector3.zero;
        yield return new WaitForSeconds(3f);
        activation = true;
    }

    // 뒤로 이동이 끝나면 호출 




    public void OnEndGoToBack()
    {
        isGoToBack = false;
    }
}
