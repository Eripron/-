using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAble : MonoBehaviour
{
    EnemyStatus enemyStat;

    void Start()
    {
        enemyStat = GetComponentInParent<EnemyStatus>();     
    }

    private void OnTriggerEnter(Collider other)
    {
        Movement player = other.GetComponent<Movement>();

        if (player == null)
            return;

        if (other.gameObject.tag == "Player")
        {
            player.Damaged(enemyStat.AttackPower);
        }
        else if (other.gameObject.tag == "Defence")
        {
            player.OnBlockAttack();
        }
    }

}
