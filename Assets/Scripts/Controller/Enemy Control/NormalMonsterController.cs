using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterController : EnemyController
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

                if (!IsPlayerFront() && !isAttack)
                    RotateToPlayer();
                else
                {
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

    public override void Damaged(int _damage)
    {
        activation = false;

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
        anim.SetTrigger("OnDie");

        // 충돌 방지 
        BoxCollider col = GetComponent<BoxCollider>();
        col.enabled = false;

        // 죽고나서 움직임 발생 x
        rigid.velocity = Vector3.zero;

        StartCoroutine(DisappearCoroutine());

    }

    IEnumerator DamagedCoroutine()
    {
        // 일반 몹만 해당함 
        isAttack = false;

        isDamaged = true;
        anim.SetTrigger("OnDamaged");

        StartCoroutine(DamagedKnockBack());

        yield return new WaitUntil(() => isDamaged == false);
        yield return new WaitForSeconds(0.7f);

        activation = true;
    }

    IEnumerator DamagedKnockBack()
    {
        Debug.Log("call back ");
        rigid.AddForce(-transform.forward * 10f, ForceMode.Impulse);
        yield return null;
    }

}
