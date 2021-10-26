using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

interface IDamaged
{
    void Damaged(int _damage);
    void Dead();
}

[RequireComponent(typeof(EnemyStatus))]
public class EnemyController : MonoBehaviour, IDamaged
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
    [SerializeField] protected Transform checkPivot;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask playerMask;

    [Header("Attack")]
    [SerializeField]
    [Range(0, 100)] protected int attackPercentage;

    protected Transform target;                               // player transform
    protected NavMeshAgent nav;                               // AI
    protected Rigidbody rigid;                                // Rigid
    protected Animator anim;                                  // Animator
    protected EnemyStatus enemyStatus;                        // Enemy Status

    // for Color Change
    SkinnedMeshRenderer[] skinMeshs;
    List<Material> matList = new List<Material>();
    List<Color> originColor = new List<Color>();


    protected bool activation = true;
    protected bool isAlive = true;
    protected bool isWalk;
    protected bool isAttack;
    protected bool isDamaged = false;

    protected float stopDistance;
    
    protected void Init()
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

        stopDistance = (checkPivot.position.z - transform.position.z) + checkRadius;
        Debug.Log(stopDistance);
        nav.stoppingDistance = stopDistance;
    }

    // move
    protected void StartMove()
    {
        nav.SetDestination(target.position);
        SetWalk(true);
    }

    protected void StopMove()
    {
        nav.ResetPath();
        rigid.velocity = Vector3.zero;

        SetWalk(false);
    }

    void SetWalk(bool _isWalk)
    {
        isWalk = _isWalk;
        anim.SetBool("IsWalk", isWalk);
    }

    // attack
    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        activation = false;

        int randomNum = new System.Random().Next(0, attackAnimName.Length);
        string animName = attackAnimName[randomNum];
        anim.Play(animName);

        int waitTime = new System.Random().Next(2, 4);
        Debug.Log($"waitTime : {waitTime}");
        yield return new WaitForSeconds(waitTime);

        isAttack = false;
        activation = true;
    }

    protected IEnumerator WaitSomeCoroutine()
    {
        activation = false;
        Debug.Log("Some Waiting");
        yield return new WaitForSeconds(2f);
        activation = true;
    }

    // 수정해야 하는 부분 
    // 보스랑 일반 몹이랑 다른 부분이다 


    // 피격 색 변화  ( boss & normal )
    protected IEnumerator DamagedColorChange()
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
    protected void ResetToOriginColor()
    {
        for (int i = 0; i < matList.Count; i++)
            matList[i].color = originColor[i];
    }



    // 적 앞에 플레이어가 있는지 없는지 판단하는 함수 
    protected bool IsPlayerFront()
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
    protected void RotateToPlayer()
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
        endPos.y -= 15f;
        Gizmos.DrawLine(checkPivot.position, endPos);
    }
   

    protected IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(2f);

        // 미구현 
        //gameObject.SetActive(false);
    }

    public virtual void Damaged(int _damage)
    {
        Debug.Log("Call Parent Damaged");
    }
    public virtual void Dead()
    {
        Debug.Log("Call Parent Dead");
    }
}
