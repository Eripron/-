using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    [SerializeField] Movement player;

    [SerializeField] int maxMp;
    [SerializeField] int maxStamina;

    [SerializeField] int aliveCount;        // 살아날수 있는 횟수 


    int mp;
    int stamina;

    bool isCheckStamina;
    bool isCharging;


    public int Mp{ get { return mp; } }
    public int Stamina { get { return stamina; } }
    public int AliveCount { get { return aliveCount; } }


    void Start()
    {
        InitStatus();
    }

    void Update()
    {
        if(stamina < maxStamina)
        {
            if(!isCheckStamina)
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

        mp = maxMp;
        stamina = maxStamina;
    }

    // stamina 사용과 회복 
    public void UseStamina(int usage)
    {
        stamina = Mathf.Clamp(stamina - usage, 0, maxStamina);
        isCharging = false;
    }
    public bool IsEnoughfStamina(int usage)
    {
        return (stamina - usage) >= 0;
    }
    IEnumerator RecoverStaminaCoroutine()
    {
        int beforeStamina = stamina;
        yield return new WaitForSeconds(1f);
        int afterStamina = stamina;

        if (beforeStamina == afterStamina)
        {
            isCharging = true;
            while(stamina < maxStamina)
            {
                if (!isCharging)
                    break;

                stamina = Mathf.Clamp(stamina += 1, 0, maxStamina);
                yield return new WaitForSeconds(0.02f);
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
    bool IsEnoughfMp(int usage)
    {
        return (mp - usage) >= 0;
    }
    void AddMp(int _mp)
    {
        mp += _mp;
        if (mp > maxMp)
            mp = maxMp;
    }
    void UseMp(int usage)
    {
        mp -= usage;
        if (mp < 0)
            mp = 0;
    }
}
