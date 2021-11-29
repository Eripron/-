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
            if(DamageTextUIManager.Instance != null)
            {
                Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z);
                DamageTextUIManager.Instance.PlayDamageText(enemyStat.AttackPower, pos, 1);
            }

            player.Damaged(enemyStat.AttackPower);
        }
        else if (other.gameObject.tag == "Defence")
        {
            player.OnBlockAttack();
        }

    }

}
