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

    [SerializeField] Movement player;

    [SerializeField] new string name;

    [SerializeField] int maxHp;
    

    [SerializeField] int attackPower;
    [SerializeField] int defensePower;

    [SerializeField] int aliveCount;

    int hp;
    int mp;
    int stamina;

    public int Hp { get { return hp; } }
    public int AliveCount => aliveCount;

    void Start()
    {
        InitStatus();
    }

    void InitStatus()
    {
        hp = maxHp;
        //mp = maxMp;
        //stamina = maxStamina;
    }

    // 데미지를 받으면 hp값 조정 
    public void OnDamaged(int damage)
    {
        hp = Mathf.Clamp(hp -= damage, 0, maxHp);
    }

    public void PlayerGetUp()
    {
        // 체력 회복 인데 확인차 code
        hp = maxHp;
        aliveCount--;

        Debug.Log($"남은 횟수 : {aliveCount} / 체력 : {hp}");
        player.OnAliveAnimation();
    }
}
