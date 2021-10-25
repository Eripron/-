using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    /*
     player, monster, boss
    - °øÅë : name, mapHp, attackPower, defencePower

    - player¸¸ 
        : maxMp, maxStamina, aliveCount, movement, 
     */

    [SerializeField] string characterName;

    [SerializeField] int maxHp;

    [SerializeField] int attackPower;
    [SerializeField] int defensePower;

    int hp;

    public int MaxHp => maxHp;
    public int Hp { get { return hp; } }
    public int AttackPower => attackPower;
    public int DefecsePower => defensePower;



    protected void InitStatus()
    {
        hp = maxHp;
    }

    void SetName(string name)
    {
        characterName = name;
    }

    public void AddHp(int _hp)
    {
        hp = Mathf.Clamp(hp += _hp, 0, maxHp);
    }

    public void OnDamaged(int damage)
    {
        hp = Mathf.Clamp((hp -= damage), 0, maxHp);
    }
    

}
