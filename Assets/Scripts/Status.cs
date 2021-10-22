using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    /*
     player, monster, boss
    - ���� : name, mapHp, attackPower, defencePower

    - player�� 
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

    // �������� ������ hp�� ���� 
    public void OnDamaged(int damage)
    {
        hp = Mathf.Clamp(hp -= damage, 0, maxHp);
    }

    public void PlayerGetUp()
    {
        // ü�� ȸ�� �ε� Ȯ���� code
        hp = maxHp;
        aliveCount--;

        Debug.Log($"���� Ƚ�� : {aliveCount} / ü�� : {hp}");
        player.OnAliveAnimation();
    }
}
