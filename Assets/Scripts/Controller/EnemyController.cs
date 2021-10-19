using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // tmp 
    int hp = 10000;

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
    bool isDamaged = false;

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
        Debug.Log("attack");

        int randomNum = new System.Random().Next(0, attackAnimName.Length);
        string animName = attackAnimName[randomNum];
        anim.Play(animName);

        float waitTime = Mathf.Clamp(randomNum + 1, 1f, 3f);
        yield return new WaitForSeconds(waitTime);

        isAttack = false;
    }

    public void Damaged(int damage)
    {
        activation = false;

        StopMove();
        StopAllCoroutines();
        anim.Rebind();

        hp -= damage;
        Debug.Log($"damaged : {damage} => HP : {hp}");


        if (hp <= 0)
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

    IEnumerator DamagedCoroutine()
    {
        isAttack = false;

        isDamaged = true;
        anim.SetTrigger("OnDamaged");

        StartCoroutine(DamagedColorChange());
        StartCoroutine(DamagedKnockBack());

        yield return new WaitUntil(() => isDamaged == false);
        yield return new WaitForSeconds(0.3f);

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
        rigid.AddForce(-transform.forward * 5f, ForceMode.Impulse);
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

        Debug.Log(isExist);
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
        anim.SetTrigger("OnDie");

        BoxCollider col = GetComponent<BoxCollider>();
        col.enabled = false;

        rigid.velocity = Vector3.zero;

        StartCoroutine(DisappearCoroutine());
        //gameObject.SetActive(false);
    }

    // 구현해야 한다.
    IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("사라지기 시작");
        float duration = 2f;

        float startTime = Time.time;
        float endTime = startTime + duration;
        float elapsedTime = 0f;

        while(Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime;

            foreach (MeshRenderer mesh in meshs)
            {
                Color changeAlpha = mesh.material.color;
                Debug.Log($"{elapsedTime / duration}");
                changeAlpha.a -= (elapsedTime/ duration);
                mesh.material.color = changeAlpha;
            }
            yield return null;
        }
    }

}
