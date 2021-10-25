using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStatus))]
public class EnemyController : MonoBehaviour
{

    Transform target;

    [SerializeField] string[] attackAnimName;

    [SerializeField] Transform checkPivot;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask playerMask;

    NavMeshAgent nav;                               // 이동 관련
    Rigidbody rigid;
    Animator anim;

    EnemyStatus enemyStatus;

    List<Material> matList = new List<Material>();

    MeshRenderer[] meshs;                           // model 전체 색상 관리 
    SkinnedMeshRenderer[] skinMeshs;

    List<Color> originColor = new List<Color>();


    bool activation = true;                         // 아직 쓰지는 않는다.

    bool isWalk;
    bool isAttack;
    bool isDamaged = false;

    void Start()
    {
        target = FindObjectOfType<PlayerStatus>().transform;

        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        enemyStatus = GetComponent<EnemyStatus>();

        meshs = GetComponentsInChildren<MeshRenderer>();
        skinMeshs = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (MeshRenderer m in meshs)
            matList.Add(m.material);
        foreach (SkinnedMeshRenderer sm in skinMeshs)
            matList.Add(sm.material);

        foreach (SkinnedMeshRenderer mesh in skinMeshs)
            originColor.Add(mesh.material.color);

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


    public void Damaged(int damage)
    {
        activation = false;

        StopAllCoroutines();
        ResetToOriginColor();

        StopMove();
        anim.Rebind();

        enemyStatus.OnDamaged(damage);

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

    IEnumerator DamagedCoroutine()
    {
        isAttack = false;

        isDamaged = true;
        anim.SetTrigger("OnDamaged");

        StartCoroutine(DamagedKnockBack());

        yield return new WaitUntil(() => isDamaged == false);
        yield return new WaitForSeconds(0.7f);

        activation = true;
    }

    // 피격 효과 
    IEnumerator DamagedColorChange()
    {

        foreach (SkinnedMeshRenderer mesh in skinMeshs)
        {
            originColor.Add(mesh.material.color);
            Color changeColor = Color.red;
            changeColor.a = 0.3f;
            mesh.material.color = changeColor;
        }

        yield return new WaitForSeconds(0.05f);

        ResetToOriginColor();
    }

    void ResetToOriginColor()
    {
        for (int i = 0; i < skinMeshs.Length; i++)
        {
            skinMeshs[i].material.color = originColor[i];
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

        // 충돌 방지 
        BoxCollider col = GetComponent<BoxCollider>();
        col.enabled = false;

        // 죽고나서 움직임 발생 x
        rigid.velocity = Vector3.zero;

        StartCoroutine(DisappearCoroutine());
    }

    IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(2f);

        float time = 2f;

        while(time > 0f)
        {

            Color originColor = meshs[0].material.color;
            originColor.a = time / 2f;
            for(int i=0; i<meshs.Length; i++)
            {
                meshs[i].material.color = originColor;
                yield return null;
            }

            time -= Time.deltaTime;
        }

        // 사라 지는것을 확인 
        //gameObject.SetActive(false);

    }

}
