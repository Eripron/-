using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerStatus : Status
{
    Movement player;

    [SerializeField] int maxSP;
    [SerializeField] int maxStamina;
    [SerializeField] int aliveCount;        // ��Ƴ��� �ִ� Ƚ�� 

    DeadUIManager deadUiManager;
    StatusUiManager statusUIManager;

    int sp;
    int stamina;

    bool isCheckStamina;
    bool isCharging;
    bool isAlive = true;

    public override int Hp
    {
        get => base.Hp;
        set
        {
            base.Hp = value;
            if(Hp <= 0)
            {
                if (!isAlive)
                    return;

                isAlive = false;

                deadUiManager.OnSetTarget(transform);
                deadUiManager.SetDeadUI(true, aliveCount);
            }

            StatusUiManager.Instance.SetHpUI(hp, MaxHp);


            MapInfoUI.Instance.OnSetEachPlayerHpGage(Name, Hp, MaxHp);
        }
    }

    public int SP
    {
        get { return sp; }
        private set
        {
            sp = Mathf.Clamp(value, 0, maxSP);
            statusUIManager.SetSpUI(sp, maxSP);
        }
    }
    public int Stamina
    {
        get
        {
            return stamina;
        }
        private set
        {
            stamina = Mathf.Clamp(value, 0, maxStamina);

            statusUIManager.SetStaminaUI(stamina, maxStamina);
        }
    }
    public int AliveCount { get { return aliveCount; } }


    void Start()
    {
        InitStatus();
    }

    void Update()
    {
        if (stamina < maxStamina)
        {
            if (!isCheckStamina)
            {
                isCheckStamina = true;
                StartCoroutine(RecoverStaminaCoroutine());
            }
        }
    }

    // �ʱ�ȭ 
    new void InitStatus()
    {
        player = Movement.Instance;
        statusUIManager = StatusUiManager.Instance;
        deadUiManager = FindObjectOfType<DeadUIManager>();

        base.InitStatus();

        SP = 0;
        Stamina = maxStamina;
    }

    // stamina ���� ȸ�� 
    public void UseStamina(int usage)
    {
        Stamina -= usage;
        isCharging = false;
    }
    public bool IsEnoughfStamina(int usage)
    {
        return (stamina - usage) >= 0;
    }

    WaitForSeconds staminaWaitTime = new WaitForSeconds(0.02f);
    IEnumerator RecoverStaminaCoroutine()
    {
        int beforeStamina = stamina;
        yield return new WaitForSeconds(0.7f);
        int afterStamina = stamina;

        if (beforeStamina == afterStamina)
        {
            isCharging = true;
            while (stamina < maxStamina)
            {
                if (!isCharging)
                    break;

                Stamina += 1;
                yield return staminaWaitTime;
            }
        }

        isCheckStamina = false;
    }

    // player ��Ƴ��� �ִ��� �Ǵ� �� ���� ��� 
    public bool isEnoughfLife()
    {
        return aliveCount > 0;
    }


    // ���� ��� (��ų���� ���� �Һ�)
    bool IsEnoughfSp(int usage)
    {
        return (sp - usage) >= 0;
    }
    public void AddSp(int _sp)
    {
        SP += _sp;
        if (sp > maxSP)
            SP = maxSP;
    }
    void UseSp(int usage)
    {
        SP -= usage;
        if (sp < 0)
            SP = 0;
    }

    public override void OnDamaged(int damage)
    {
        Hp -= damage;
    }


    public void OnRevivePlayer()
    {
        if (aliveCount <= 0)
            return;

        isAlive = true;
        // count ��� hp ü��� 
        aliveCount--;
        AddHp(MaxHp);

        player.OnPlayerRevive();
        deadUiManager.SetDeadUI(false);
    }
}
