using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    // 사용하나 ?
    Movement player;

    [SerializeField] int maxSP;
    [SerializeField] int maxStamina;

    [SerializeField] int aliveCount;        // 살아날수 있는 횟수 

    StatusUiManager statusUIManager;

    int sp;
    int stamina;

    bool isCheckStamina;
    bool isCharging;


    public override int Hp
    {
        get => base.Hp;
        set
        {
            base.Hp = value;
            StatusUiManager.Instance.SetHpUI(hp, MaxHp);
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

    // 초기화 
    new void InitStatus()
    {
        base.InitStatus();

        statusUIManager = StatusUiManager.Instance;
        SP = 0;
        Stamina = maxStamina;
    }

    // stamina 사용과 회복 
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

    // player 살아날수 있는지 판단 및 생명 사용 
    public bool isEnoughfLife()
    {
        return aliveCount > 0;
    }
    public void UseLife()
    {
        aliveCount--;
    }


    // 마나 사용 (스킬사용시 마나 소비)
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

}
