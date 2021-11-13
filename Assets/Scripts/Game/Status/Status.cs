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

    protected int hp;

    // player 인가?

    public string Name => characterName;
    public int MaxHp => maxHp;
    public virtual int Hp
    {
        get 
        { 
            return hp; 
        }
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
        }
    }
    public int AttackPower => attackPower;
    public int DefecsePower => defensePower;



    protected void InitStatus()
    {
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

    public virtual void OnDamaged(int damage)
    {
        Hp -= damage;
    }


}
