using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] string characterName;

    [SerializeField] int attackPower;
    [SerializeField] int defensePower;

    [SerializeField] int maxHp;


    protected int hp;

    public string Name => characterName;
    public int MaxHp 
    {
        get { return maxHp; }
        set { maxHp = value; } 
    }

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
    public int AttackPower { get { return attackPower; } set { attackPower = value; } }
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
