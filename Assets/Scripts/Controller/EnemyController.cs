using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Singleton<EnemyController>
{
    [SerializeField] Transform target;
    [SerializeField] string[] attackAnimName;


    [SerializeField] Transform checkPivot;
    [SerializeField] float r;
    [SerializeField] LayerMask playerMask;

    MeshRenderer[] meshs;                           // model 전체 색상 관리 
    NavMeshAgent nav;                               // 이동 관련
    Rigidbody rigid;
    Animator anim;

    bool activation = true;

    bool isWalk;
    bool isAttack;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (target == null)
            return;

        /*
         플레이어를 향해 이동 
         거리가 가까워 지면 멈추고 공격 -> 공격후 (다시 공격,  이동)
         공격 도중 데미지를 받으면 공격 멈추고 피격당함 -> 피격당하고도 다음 모션까지 약간의 딜레이 존재 

        hp가 0되면 이동을 멈추고 죽는다.
         */

        if (activation)
        {
            Move();
        }
        else
        {
            isWalk = false;
        }
        anim.SetBool("IsWalk", isWalk);

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= nav.stoppingDistance)
        {
            if (!isAttack && CheckPlayer())
                StartCoroutine(AttackCoroutine());
            else
                transform.LookAt(target.position);

        }
    }


    void Move()
    {
        isWalk = true;
        nav.SetDestination(target.position);
    }

    IEnumerator AttackCoroutine()
    {
        activation = false;
        isAttack = true;

        int randomNum = new System.Random().Next(0, attackAnimName.Length);

        string animName = attackAnimName[randomNum];
        anim.Play(animName);

        yield return new WaitForSeconds(2f);
        isAttack = false;
        activation = true;
    }


    public void Damaged()
    {
        StopAllCoroutines();

        isAttack = false;
        activation = false;

        StartCoroutine(DamagedColorChange());
        StartCoroutine(DamagedKnockBack());

        activation = true;
    }


    IEnumerator DamagedColorChange()
    {
        List<Color> originColor = new List<Color>();

        foreach(MeshRenderer mesh in meshs)
        {
            originColor.Add(mesh.material.color);
            Color changeColor = Color.red;
            changeColor.a = 0.3f;
            mesh.material.color = changeColor;
        }

        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < meshs.Length; i++)
        {
            meshs[i].material.color = originColor[i];
        }
    }
    IEnumerator DamagedKnockBack(/*Vector3 knockBackDir*/)
    {
        Vector3 curPosition = transform.position;
        rigid.AddForce(-transform.forward * 6f, ForceMode.Impulse);
        yield return null;
    }


    bool CheckPlayer()
    {
        RaycastHit hit;
        if(Physics.SphereCast(checkPivot.position, r, Vector3.down, out hit, 3f, playerMask))
        {
            Debug.Log("플레이어 앞에 있음");
            return true;
        }
        Debug.Log("플레이어 앞에 없음");
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(checkPivot.position, r);
        Vector3 endPos = checkPivot.position;
        endPos.y -= 3f;
        Gizmos.DrawLine(checkPivot.position, endPos);
    }

    void Dead()
    {
        // play dead animation 
    }

}
