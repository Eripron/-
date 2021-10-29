using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterController : EnemyController
{
    bool isGoToBack = false;
    int layer;

    Coroutine coWaitMoment;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (target == null || !isAlive)
            return;

        CheckDistanceToPlayer();

        // �������� �ƴϰ� �����ٸ� �÷��̾ ���� ȸ�� 
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
                else
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
                waitDir = (-transform.forward) + transform.right;
                break;
            case 1:
                waitDir = (-transform.right);
                break;
            case 2:
                waitDir = (-transform.forward) + (-transform.right);
                break;
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

    // �ڷ� �̵��� ������ ȣ�� 
    public void OnEndGoToBack()
    {
        isGoToBack = false;
    }
}
