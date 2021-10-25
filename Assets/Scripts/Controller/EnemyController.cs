using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStatus))]
public class EnemyController : MonoBehaviour
{
    // 손봐야 하는 부분 적의 이동이랑 공격 거리 설정 

    /*
     보스 vs 일반 몹 차이 
    
    Boss -> 1. 일정 데미지 이상 => knock down
         -> 2. 포효하는 모션 추가 
         -> ** 피격시 색 변화는 있지만 뒤로 넉백은 없음 
     */

    // to attack 
    [SerializeField] AnimationClip[] animationClips;
    string[] attackAnimName;

    [Header("Check Player")]
    [SerializeField] Transform checkPivot;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask playerMask;

    
    Transform target;                               // player transform
    NavMeshAgent nav;                               // AI
    Rigidbody rigid;                                // Rigid
    Animator anim;                                  // Animator
    EnemyStatus enemyStatus;                        // Enemy Status

    // for Color Change
    SkinnedMeshRenderer[] skinMeshs;
    List<Material> matList = new List<Material>();
    List<Color> originColor = new List<Color>();


    bool activation = true;                         // 아직 쓰지는 않는다.

    bool isWalk;
    bool isAttack;
    bool isDamaged = false;


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
    void Init()
    {
        target = FindObjectOfType<PlayerStatus>().transform;
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        enemyStatus = GetComponent<EnemyStatus>();

        skinMeshs = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer mesh in skinMeshs)
        {
            matList.Add(mesh.material);
            originColor.Add(mesh.material.color);
        }

        attackAnimName = new string[animationClips.Length];
        for (int i = 0; i < animationClips.Length; i++)
        {
            attackAnimName[i] = animationClips[i].name;
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

    // 수정해야 하는 부분 
    // 보스랑 일반 몹이랑 다른 부분이다 
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
        // 일반 몹만 해당함 
        isAttack = false;

        isDamaged = true;
        anim.SetTrigger("OnDamaged");

        StartCoroutine(DamagedKnockBack());

        yield return new WaitUntil(() => isDamaged == false);
        yield return new WaitForSeconds(0.7f);

        activation = true;
    }

    // 피격 색 변화  ( boss & normal )
    IEnumerator DamagedColorChange()
    {
        Color damagedColor = Color.red;

        foreach (Material mat in matList)
        {
            damagedColor.a = 0.1f;
            mat.color = damagedColor;
        }
        yield return new WaitForSeconds(0.05f);

        ResetToOriginColor();
    }
    void ResetToOriginColor()
    {
        for (int i = 0; i < matList.Count; i++)
            matList[i].color = originColor[i];
    }


    // knock back ( only normal )
    IEnumerator DamagedKnockBack()
    {
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


        // 사라 지는것을 확인 
        //gameObject.SetActive(false);

    }

}
