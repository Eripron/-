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

    Collider[] colliders;

    // to attack 
    [SerializeField] AnimationClip[] animationClips;
    string[] attackAnimName;

    [Header("Check Player")]
    [SerializeField] protected Transform checkPivot;
    [SerializeField] Transform checkPivot2;
    [SerializeField] [Range(0f, 10f)] float checkRadius;
    [SerializeField] LayerMask playerMask;

    [Header("Attack")]
    [SerializeField]
    [Range(0, 100)] protected int attackPercentage;

    [Header("Move")]
    [SerializeField] float rotateSpeed;

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
    protected bool isWalk;
    protected bool isAttack;
    protected bool isWait = false;
    protected bool isAlive = true;
    protected bool isFar;

    protected float stopDistance;
    protected bool isDamaged = false;


    protected Vector3 waitDir = Vector3.zero;

    protected void Init()
    {
        target      = FindObjectOfType<PlayerStatus>().transform;

        nav         = GetComponent<NavMeshAgent>();
        rigid       = GetComponent<Rigidbody>();
        enemyStatus = GetComponent<EnemyStatus>();

        anim        = GetComponentInChildren<Animator>();
        skinMeshs   = GetComponentsInChildren<SkinnedMeshRenderer>();
        colliders   = GetComponentsInChildren<Collider>();

        damagedColorWait = new WaitForSeconds(0.05f);

        foreach (SkinnedMeshRenderer mesh in skinMeshs)
        {
            matList.Add(mesh.material);
            originColor.Add(mesh.material.color);
        }

        attackAnimName = new string[animationClips.Length];
        for (int i = 0; i < animationClips.Length; i++)
            attackAnimName[i] = animationClips[i].name;


        stopDistance = (checkPivot.localPosition.z) + checkRadius;
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
        rigid.velocity = Vector3.zero;
        nav.ResetPath();
        SetWalk(false);
    }
    void SetWalk(bool _isWalk)
    {
        isWalk = _isWalk;
        anim.SetBool("IsWalk", isWalk);
    }
    protected void CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        isFar = (distance > nav.stoppingDistance) ? true : false;
    }

    // attack
    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        activation = false;

        int randomNum = new System.Random().Next(0, attackAnimName.Length);
        string animName = attackAnimName[randomNum];
        anim.Play(animName);

        int waitTime = Random.Range(2, 4);
        yield return new WaitForSeconds(waitTime);

        isAttack = false;
        activation = true;
    }

    // 적 앞에 플레이어가 있는지 없는지 판단하는 함수 
    protected bool IsPlayerFront()
    {
        bool isExist = false;

        RaycastHit hit;
        if (Physics.SphereCast(checkPivot.position, checkRadius, Vector3.down, out hit, float.MaxValue, playerMask))
        {
            CharacterController player = hit.transform.GetComponent<CharacterController>();

            if(Physics.Raycast(new Ray(checkPivot2.position, transform.forward), float.MaxValue, playerMask))
            {
                if (player != null)
                    isExist = true;
            }
        }

        return isExist;
    }
    // player를 향해 회전 
    protected void RotateToPlayer(bool fastAngle = false)
    {
        Vector3 dir = target.position - transform.position;
        Vector3 turnDirection;
        if (fastAngle)
            turnDirection = Vector3.RotateTowards(transform.forward, dir, 270f * Time.deltaTime, 0f);
        else
            turnDirection = Vector3.RotateTowards(transform.forward, dir, rotateSpeed * Time.deltaTime, 0f);

        Quaternion rotation = Quaternion.LookRotation(turnDirection);
        transform.rotation = rotation;
    }

    // **************************
    // 묶을수 있는 거는 한번에 묶어서 신경 안쓰도록 하는게 좋다.
    Coroutine coDamagedColor;
    WaitForSeconds damagedColorWait;
        
    IEnumerator DamagedColor()
    {
        Color damagedColor = Color.red;
        foreach (Material mat in matList)
        {
            damagedColor.a = 0.1f;
            mat.color = damagedColor;
        }
        yield return damagedColorWait;

        ResetToOriginColor();
    }
    private void ResetToOriginColor()
    {
        for (int i = 0; i < matList.Count; i++)
            matList[i].color = originColor[i];
    }
    public virtual void Damaged(int _damage)
    {

        // 피격 색 변화  ( boss & normal )
        // change color when damaged.
        if (coDamagedColor != null)
            StopCoroutine(coDamagedColor);

        coDamagedColor = StartCoroutine(DamagedColor());

        enemyStatus.OnDamaged(_damage);
    }

    public virtual void Dead()
    {
        isAlive = false;

        foreach (var col in colliders)
            col.enabled = false;

        anim.Rebind();
        anim.SetTrigger("OnDie");

        rigid.velocity = Vector3.zero;

        ResetToOriginColor();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(checkPivot.position, checkRadius);
        Vector3 endPos = checkPivot.position;
        endPos.y -= 15f;
        Gizmos.DrawLine(checkPivot.position, endPos);

        Gizmos.DrawLine(checkPivot2.position, transform.forward * 20f);
    }
}
