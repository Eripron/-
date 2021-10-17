using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
<<<<<<< HEAD
    /*
     player, monster, boss
    - 공통 : 이름, 체력, 공격력, 방어력
    
    player
    - 마나, 스태미나, 
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
=======


>>>>>>> 25ad161bc65819a309263c14325d9b29ad887534

}
