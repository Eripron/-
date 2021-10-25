using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStatus))]
public class EnemyController : MonoBehaviour
{
    // �պ��� �ϴ� �κ� ���� �̵��̶� ���� �Ÿ� ���� 

    /*
     ���� vs �Ϲ� �� ���� 
    
    Boss -> 1. ���� ������ �̻� => knock down
         -> 2. ��ȿ�ϴ� ��� �߰� 
         -> ** �ǰݽ� �� ��ȭ�� ������ �ڷ� �˹��� ���� 
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


    bool activation = true;                         // ���� ������ �ʴ´�.

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

    // �����ؾ� �ϴ� �κ� 
    // ������ �Ϲ� ���̶� �ٸ� �κ��̴� 
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
        // �Ϲ� ���� �ش��� 
        isAttack = false;

        isDamaged = true;
        anim.SetTrigger("OnDamaged");

        StartCoroutine(DamagedKnockBack());

        yield return new WaitUntil(() => isDamaged == false);
        yield return new WaitForSeconds(0.7f);

        activation = true;
    }

    // �ǰ� �� ��ȭ  ( boss & normal )
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


    // �� �տ� �÷��̾ �ִ��� ������ �Ǵ��ϴ� �Լ� 
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
    // player�� ���� ȸ�� 
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

        // �浹 ���� 
        BoxCollider col = GetComponent<BoxCollider>();
        col.enabled = false;

        // �װ��� ������ �߻� x
        rigid.velocity = Vector3.zero;

        StartCoroutine(DisappearCoroutine());

    }

    IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(2f);


        // ��� ���°��� Ȯ�� 
        //gameObject.SetActive(false);

    }

}
