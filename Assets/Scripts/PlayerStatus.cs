using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    [SerializeField] Movement player;

    [SerializeField] int maxMp;
    [SerializeField] int maxStamina;

    [SerializeField] int aliveCount;        // ��Ƴ��� �ִ� Ƚ�� 


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

    // �ʱ�ȭ 
    new void InitStatus()
    {
        base.InitStatus();

        mp = maxMp;
        stamina = maxStamina;
    }

    // stamina ���� ȸ�� 
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

    // player ��Ƴ��� �ִ��� �Ǵ� �� ���� ��� 
    public bool isEnoughfLife()
    {
        return aliveCount > 0;
    }
    public void UseLife()
    {
        aliveCount--;
    }


    // ���� ��� (��ų���� ���� �Һ�)
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
