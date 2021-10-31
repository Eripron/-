using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    /*
     player, monster, boss
    - 공통 : name, mapHp, attackPower, defencePower

    - player만 
        : maxMp, maxStamina, aliveCount, movement, 
     */

    [SerializeField] string characterName;

    [SerializeField] int attackPower;
    [SerializeField] int defensePower;

    [SerializeField] int maxHp;

    int hp;

    // player 인가?
    bool isPlayer = false;

    public string Name => characterName;
    public int MaxHp => maxHp;
    public virtual int Hp
    {
        get 
        { 
            return hp; 
        }
        private set
        {
            hp = Mathf.Clamp(value, 0, maxHp);

            if(isPlayer)
            {
                Debug.Log("call hp set");
                StatusUiManager.Instance.SetHpUI(hp, MaxHp);
            }
        }
    }
    public int AttackPower => attackPower;
    public int DefecsePower => defensePower;



    protected void InitStatus()
    {
        Movement player = GetComponent<Movement>();
        if (player != null)
        {
            isPlayer = true;
        }

        Hp = maxHp;
    }

    void SetName(string name)
    {
        characterName = name;
    }

    public void AddHp(int _hp)
    {
        Hp += _hp;
    }

    public void OnDamaged(int damage)
    {
        Hp -= damage;
    }


}
