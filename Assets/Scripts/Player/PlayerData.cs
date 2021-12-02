using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    [SerializeField] int attackPower;
    [SerializeField] int defensePower;

    [SerializeField] int maxHP;
    [SerializeField] int maxStamina;
    [SerializeField] int maxSp;
    [SerializeField] int aliveCount;


    public int Power => attackPower;
    public int MaxHp => maxHP;
    public int MaxStamina => maxStamina;
    public int MaxSp => maxSp;
    public int AliveCount => aliveCount;

    new void Awake()
    {
        base.Awake();    
    }

}
