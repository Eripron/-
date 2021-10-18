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
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask playerMask;

    MeshRenderer[] meshs;                           // model 전체 색상 관리 
    NavMeshAgent nav;                               // 이동 관련
    Rigidbody rigid;
    Animator anim;

    bool activation = true;                         // 아직 쓰지는 않는다.

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

        if (activation)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= nav.stoppingDistance)
            {
                StopMove();

                if (!IsPlayerFront())
                    RotateToPlayer();
                else
                {
                    if (!isAttack)
                        StartCoroutine(AttackCoroutine());


                }
            }
            else if (distance > nav.stoppingDistance)
            {
                StartMove();
            }
        }
    }


    // move
    void StartMove()
    {
        isWalk = true;
        nav.SetDestination(target.position);
        anim.SetBool("IsWalk", isWalk);
    }
    void StopMove()
    {
        isWalk = false;
        nav.ResetPath();
        anim.SetBool("IsWalk", isWalk);
    }

    // attack
    IEnumerator AttackCoroutine()
    {
        isAttack = true;

        int randomNum = new System.Random().Next(0, attackAnimName.Length);
        string animName = attackAnimName[randomNum];
        anim.Play(animName);

        float waitTime = Mathf.Clamp(randomNum + 1, 1f, 3f);
        yield return new WaitForSeconds(waitTime);

        isAttack = false;
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

    // 피격 효과 
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
    // 넉백 효과 
    IEnumerator DamagedKnockBack(/*Vector3 knockBackDir*/)
    {
        Vector3 curPosition = transform.position;
        rigid.AddForce(-transform.forward * 6f, ForceMode.Impulse);
        yield return null;
    }


    // 적 앞에 플레이어가 있는지 없는지 판단하는 함수 
    bool IsPlayerFront()
    {
        bool isExist = false;

        RaycastHit hit;
        if (Physics.SphereCast(checkPivot.position, checkRadius, Vector3.down, out hit, float.MaxValue, playerMask))
        {
            CharacterController player = hit.transform.GetComponent<CharacterController>();

            if (player != null)
                isExist = true;
        }

        return isExist;
    }
    // player를 향해 회전 
    void RotateToPlayer()
    {
        Vector3 dir = target.position - transform.position;

        Vector3 turnDirection = Vector3.RotateTowards(transform.forward, dir, 40f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(turnDirection);
        transform.rotation = rotation;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(checkPivot.position, checkRadius);
        Vector3 endPos = checkPivot.position;
        endPos.y -= 6f;
        Gizmos.DrawLine(checkPivot.position, endPos);
    }

    void Dead()
    {
        // play dead animation 
    }

}
