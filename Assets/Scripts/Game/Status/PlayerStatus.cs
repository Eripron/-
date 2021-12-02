using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerStatus : Status
{
    PlayerData playerData;
    Movement player;

    [SerializeField] int maxSP;
    [SerializeField] int maxStamina;
    [SerializeField] int aliveCount;        // 살아날수 있는 횟수 

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
            if (Hp <= 0)
            {
                if (!isAlive)
                    return;

                isAlive = false;

                if (deadUiManager == null)
                    deadUiManager = DeadUIManager.Instance;

                if (deadUiManager)
                {
                    deadUiManager.OnSetTarget(transform);
                    deadUiManager.SetDeadUI(true, aliveCount);
                }
            }

            StatusUiManager.Instance.SetHpUI(hp, MaxHp);

            // ------------- 고쳐야 한다.
            if(MapInfoUI.Instance != null)
                MapInfoUI.Instance.OnChangePlayerHpGage(Name, Hp, MaxHp);
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
        playerData = PlayerData.Instance;
        statusUIManager = StatusUiManager.Instance;
        player = GetComponent<Movement>();

        QuickSlotUIManager.Instance.OnQuickSlotUI();

        AttackPower = playerData.Power;

        MaxHp = playerData.MaxHp;
        Hp = MaxHp;
        maxSP = playerData.MaxSp;
        SP = 0;


        maxStamina = playerData.MaxStamina;
        Stamina = maxStamina; ;

        aliveCount = playerData.AliveCount;
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


    public void OnRevivePlayer()
    {
        if (aliveCount <= 0)
            return;

        isAlive = true;
        // count 깍고 hp 체우고 
        aliveCount--;
        AddHp(MaxHp);

        player.OnPlayerRevive();
        deadUiManager.SetDeadUI(false);
    }
}
