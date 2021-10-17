using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public SkinnedMeshRenderer[] meshs;

    [Header("Ground Check")]
    [SerializeField] Transform groundPivot;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundRadius;

    [Header("Move")]
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float dashSpeed;

    [SerializeField] Animator anim;

    CharacterController controller;
    Camera cam;


    float x;                    
    float z;
    float gravity = (-9.81f * 3f);

    int CountAttackClick = 0;

    Vector3 cameraForward;                  // 카메라가 바라보는 정면 방향 
    Vector3 direction;                      // 캐릭터 이동 방향 
    Vector3 velocity;                       // 속도 vector

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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;

        
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
        GroundCheck();
        SetCameraForwardDirection();

        // tmp
        if (Input.GetMouseButtonDown(1))
            EnemyController.Instance.Damaged();
        // ----------------------------------------- 


        if (isControl)
            SetCharacterDirection();

        if (!isAttack)
            Move();
        else
            controller.Move(transform.forward * Time.deltaTime * 1.1f);

        Rotation(direction);


        if (Input.GetKeyDown(KeyCode.Space) && !isStanding && !isGuard && !isDash && !isAttack)
        {
            StartCoroutine(StartDashCoroutine());
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isStanding && !isGuard && !isDash && !isAttack)
        {
            StartCoroutine(StartGuardCoroutine());
        }

        if(Input.GetMouseButtonDown(0) && !isGuard && !isDash)
        {
            //StopMove();
            Attack();
        }

        Gravity();
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

        controller.Move(direction);
    }

    void Attack()
    {
        isAttack = true;
        StopMove();

        CountAttackClick++;

        // 1st attack phase
        if(CountAttackClick == 1)
        {
            // play attack 1 motion
            anim.SetInteger("intAttackPhase", 1);
        }
    }

    void StopMove()
    {
        isControl = false;
        x = 0;
        z = 0;
    }
    void StartMove()
    {
        isControl = true;
    }

    public void CheckAttackPhase()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            if(CountAttackClick > 1)
            {
                StartMove();
                CountAttackClick = 0;
                anim.SetInteger("intAttackPhase", 2);
            }
            else
                ResetAttackPhase();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            if (CountAttackClick > 0)
            {
                StartMove();
                anim.SetInteger("intAttackPhase", 3);
            }
            else
                ResetAttackPhase();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            if (CountAttackClick > 0)
                ResetAttackPhase();
        }
    }
    void ResetAttackPhase()
    {
        isAttack = false;

        StartMove();

        CountAttackClick = 0;
        anim.SetInteger("intAttackPhase", CountAttackClick);
    }

    IEnumerator StartGuardCoroutine()
    {
        /*
         Guard 는 따로 몬스터가 공격시 막으면 hp를 깍지 않는 것을 구현해야 한다. 
         */
        isGuard = true;

        anim.SetTrigger("OnGuard");
        StopMove();

        yield return new WaitUntil(() => isGuard == false);
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
    }

    public void OnEndGuard(string str)
    {
        isGuard = false;
        StartMove();
    }
    public void OnEndDash()
    {
        isDash = false;
        StartMove();
    }


    void Rotation(Vector3 dir)
    {
        Vector3 turnDirection = Vector3.RotateTowards(transform.forward, dir, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(turnDirection);
        transform.rotation = rotation;
    }                                           // 특정 방향으로 바로보는 회전 
    void Gravity()
    {
        if (isGround && velocity.y < 0f)
            velocity.y = -5f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }                                                       // 중력 
    void GroundCheck()
    {
        isGround = Physics.CheckSphere(groundPivot.position, groundRadius, groundMask);
    }                                                   // ground check

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPivot.position, groundRadius);
    }
}
