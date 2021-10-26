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

        if (!isAttack && isAlive)
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
                    StartCoroutine(WaitSomeCoroutine());
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
        isAlive = false;
        rigid.velocity = Vector3.zero;
        activation = false;

        anim.SetTrigger("OnDie");

        // 충돌 방지 
        BoxCollider col = GetComponent<BoxCollider>();
        col.enabled = false;

        // 죽고나서 움직임 발생 x

        StartCoroutine(DisappearCoroutine());

    }

    IEnumerator DamagedCoroutine()
    {
        isAttack = false;
        isDamaged = true;

        anim.SetTrigger("OnDamaged");

        StartCoroutine(DamagedKnockBack());

        yield return new WaitUntil(() => isDamaged == false);
        yield return new WaitForSeconds(0.6f);

        activation = true;
    }

    IEnumerator DamagedKnockBack()
    {
        rigid.AddForce(-transform.forward * 10f, ForceMode.Impulse);
        yield return null;
    }


}
