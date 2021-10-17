using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    /*
     player, monster, boss
    - ���� : �̸�, ü��, ���ݷ�, ����
    
    player
    - ����, ���¹̳�, 
     */

    [SerializeField] new string name;

    [SerializeField] int maxHp;
    [SerializeField] int maxMp;
    [SerializeField] int maxStamina;

    [SerializeField] int attackPower;
    [SerializeField] int defensePower;


    int hp;
    int mp;
    int stamina;

    void Start()
    {
        hp = maxHp;
        mp = maxMp;
        stamina = maxStamina;
    }

}
