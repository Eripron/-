using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterController : EnemyController
{
    bool isGoToBack = false;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (target == null || !isAlive)
            return;

        float distance = Vector3.Distance(target.position, transform.position);
        bool isFar = (distance > nav.stoppingDistance) ? true : false;

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
                    StartCoroutine(AttackCoroutine());
                }
                else
                {
                    StartCoroutine(WaitSomeCoroutine());
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
        activation = false;
        isWait = false;

        StopAllCoroutines();
        ResetToOriginColor();

        StopMove();
        anim.Rebind();

        enemyStatus.OnDamaged(_damage);

        StartCoroutine(DamagedColorChange());
        if (enemyStatus.Hp <= 0)
        {
            Dead();
            return;
        }

        StartCoroutine(DamagedCoroutine());
    }
    public void OnDamagedEnd()
    {
        isDamaged = false;
    }

    public override void Dead()
    {
        isAlive = false;

        rigid.velocity = Vector3.zero;
        activation = false;

        anim.SetTrigger("OnDie");

        BoxCollider col = GetComponent<BoxCollider>();
        col.enabled = false;

        // 죽고나서 움직임 발생 x

        StartCoroutine(DisappearCoroutine());

    }

    IEnumerator DamagedCoroutine()
    {
        isDamaged = true;
        isAttack = false;

        anim.SetTrigger("OnDamaged");

        StartCoroutine(DamagedKnockBack());

        yield return new WaitUntil(() => isDamaged == false);
        yield return new WaitForSeconds(0.7f);

        activation = true;
    }

    IEnumerator DamagedKnockBack()
    {
        rigid.AddForce(-transform.forward * 10f, ForceMode.Impulse);
        yield return null;
    }

    protected IEnumerator WaitSomeCoroutine()
    {
        Debug.Log("waiting....");

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

        waitDir = Vector3.zero;

        isWait = false;
        activation = true;
    }

    // 뒤로 이동이 끝나면 호출 
    public void OnEndGoToBack()
    {
        isGoToBack = false;
    }
}
