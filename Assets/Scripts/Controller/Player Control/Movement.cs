using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LAYER
{
    LAYER_PLAYER = 7,
    LAYER_DEAD = 8,
}

[RequireComponent(typeof(PlayerStatus))]
public class Movement : MonoBehaviour
{

    // tmp  ------------------------------------------------
    //------------------------------------------------------
    public SkinnedMeshRenderer[] meshs;

    [Header("Ground Check")]
    [SerializeField] Transform groundPivot;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundRadius;

    [Header("Move")]
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float dashSpeed;

    [Header("Status")]
    [SerializeField] int dashStaminaUsage;
    [SerializeField] int attackStaminaUsage;

    Animator anim;

    CharacterController controller;
    Camera cam;

    PlayerStatus status;


    float x;                    
    float z;
    float gravity = (-9.81f * 3f);

    int CountAttackClick = 0;
    

    Vector3 cameraForward;                  // ī�޶� �ٶ󺸴� ���� ���� 
    Vector3 direction;                      // ĳ���� �̵� ���� 
    Vector3 velocity;                       // �ӵ� vector

    Quaternion rotation;

    bool isStanding;
    bool isAccel;
    bool isWalk;
    bool isRun;
    bool isGround;
    bool isDash = false;
    bool isGuard = false;
    bool isControl = true;
    bool isAttack = false;
    bool isDamaged = false;
    bool isAlive = true;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        status = GetComponent<PlayerStatus>();
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>();

        /*
        foreach (SkinnedMeshRenderer c in meshs)
        {
            c.material.color = Color.red;
        }
        */
    }

    void Update()
    {
        if (!isAlive && status.isEnoughfLife() && Input.GetKeyDown(KeyCode.R))
        {
            isAlive = true;

            status.UseLife();
            PlayerGetUp(status.MaxHp);
        }

        if (!isAlive)
            return;

        GroundCheck();
        SetCameraForwardDirection();

        if (isControl)
            SetCharacterDirection();            // == get input

        Move();

        if(isAttack)
            controller.Move(transform.forward * Time.deltaTime * 1.5f);

        Rotation(direction);


        if (Input.GetKeyDown(KeyCode.Space) && !isStanding && !isGuard && !isDash && !isAttack && !isDamaged)
        {
            if (status.IsEnoughfStamina(dashStaminaUsage))
            {
                StartCoroutine(StartDashCoroutine());
                status.UseStamina(10);
            }
            else
                Debug.Log("���׹̳� ����");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isStanding && !isGuard && !isDash && !isAttack && !isDamaged)
        {
            StartCoroutine(StartGuardCoroutine());
        }

        if((Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0)) && !isGuard && !isDash && !isDamaged)
        {
            Attack();
        }

        Gravity();
    }

    public void Damaged(int damage)
    {
        StopAllCoroutines();
        StopMove();

        isDamaged = true;
        isDash = false;
        isGuard = false;
        ResetAttackPhase();

        anim.SetTrigger("OnDamaged");

        status.OnDamaged(damage);
        if(status.Hp <= 0)
        {
            Dead();
        }

        StartCoroutine(DamagedCoroutine());
    }

    IEnumerator DamagedCoroutine()
    {
        yield return new WaitUntil(() => isDamaged == false);
        StartMove();
    }

    void Dead()
    {
        isAlive = false;
        controller.detectCollisions = false;

        gameObject.layer = (int)LAYER.LAYER_DEAD;

        anim.SetTrigger("OnDead");
    }

    public void OnEndDamage()
    {
        isDamaged = false;
    }

    void SetCharacterDirection()
    {
        isAccel = Input.GetKey(KeyCode.LeftShift);

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        float accel = isAccel ? 1.5f : 1.0f;

        direction = cameraForward * z * moveSpeed * accel * Time.deltaTime;
        direction += Quaternion.Euler(0, 90, 0) * cameraForward * x * moveSpeed * accel * Time.deltaTime;

        isStanding = direction != Vector3.zero ? false : true;
    }
    void SetCameraForwardDirection()
    {
        cameraForward = cam.transform.rotation * Vector3.forward;
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;
    }
    void Move()
    {
        isWalk = (!isStanding && !isAccel) ? true : false;
        isRun = (!isStanding && isAccel) ? true : false;

        anim.SetBool("IsWalk", isWalk);
        anim.SetBool("IsRun", isRun);

        if (!isAttack && !isDamaged && !isDash && !isGuard)
            controller.Move(direction);
    }

    void Attack()
    {
        CountAttackClick++;

        if(isControl)
            StopMove();

        if (CountAttackClick >= 1 && !isAttack && status.IsEnoughfStamina(attackStaminaUsage))
        {
            isAttack = true;
            anim.SetInteger("intAttackPhase", 1);
            status.UseStamina(attackStaminaUsage);
        }
        else
        {
            StartMove();
        }
    }


    void StopMove()
    {
        isControl = false;
        isStanding = true;

        x = 0;
        z = 0;
    }
    void StartMove()
    {
        isControl = true;
        SetCharacterDirection();
    }

    public void CheckAttackPhase()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            if(CountAttackClick > 1 && status.IsEnoughfStamina(attackStaminaUsage))
            {
                StartMove();
                CountAttackClick = 0;
                anim.SetInteger("intAttackPhase", 2);

                status.UseStamina(attackStaminaUsage);
            }
            else
                ResetAttackPhase();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            if (CountAttackClick > 0 && status.IsEnoughfStamina(attackStaminaUsage))
            {
                StartMove();
                anim.SetInteger("intAttackPhase", 3);

                status.UseStamina(attackStaminaUsage);
            }
            else
                ResetAttackPhase();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            if (CountAttackClick >= 0)
            {
                ResetAttackPhase();
            }
        }
    }

    void ResetAttackPhase()
    {
        CountAttackClick = 0;
        anim.SetInteger("intAttackPhase", 0);


        if(!isDamaged)
            StartMove();

        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.05f);
        isAttack = false;
    }

    IEnumerator StartGuardCoroutine()
    {
        StopMove();
        isGuard = true;
        anim.SetTrigger("OnGuard");

        yield return new WaitUntil(() => isGuard == false);
        StartMove();
    }

    public void OnBlockAttack()
    {
        anim.SetTrigger("OnGuardHit");
    }

    IEnumerator StartDashCoroutine()     
    {
        StopMove();
        isDash = true;

        anim.SetTrigger("OnDash");

        while (isDash)
        {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
        StartMove();
    }

    public void OnEndGuard()
    {
        isGuard = false;
    }
    public void OnEndDash()
    {
        isDash = false;
    }

    void Rotation(Vector3 dir)
    {
        Vector3 turnDirection = Vector3.RotateTowards(transform.forward, dir, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(turnDirection);
        transform.rotation = rotation;
    }                                           // Ư�� �������� �ٷκ��� ȸ�� 
    void Gravity()
    {
        if (isGround && velocity.y < 0f)
            velocity.y = -5f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }                                                       // �߷� 
    void GroundCheck()
    {
        isGround = Physics.CheckSphere(groundPivot.position, groundRadius, groundMask);
    }                                                   // ground check


    // �ܺο��� ȣ���ϱ⸸ �ϸ� ��
    void PlayerGetUp(int _hp)
    {
        status.AddHp(_hp);
        anim.SetTrigger("OnAlive");
    }

    IEnumerator ResetCoroutine()
    {
        isControl = true;
        isDamaged = false;
        isDash = false;
        isGuard = false;
        ResetAttackPhase();
        controller.detectCollisions = true;

        yield return new WaitForSeconds(2f);

        Debug.Log("change layer");
        gameObject.layer = (int)LAYER.LAYER_PLAYER;
    }

    public void AllReset()
    {
        StartCoroutine(ResetCoroutine());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPivot.position, groundRadius);
    }
}