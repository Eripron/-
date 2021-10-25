using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAble : MonoBehaviour
{
    [SerializeField] float attackRadius;
    [SerializeField] Transform startAttackPivot;
    [SerializeField] Transform endAttackPivot;
    [SerializeField] LayerMask enemyLayer;

    PlayerStatus playerStatus;

    // 검색한 적 저장
    List<EnemyController> enemys = new List<EnemyController>();


    void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();     
    }

    public void OnCheckEnemyInAttackArea()
    {
        Collider[] colliders = Physics.OverlapCapsule(startAttackPivot.position, endAttackPivot.position, attackRadius, enemyLayer);

        foreach(Collider col in colliders)
        {
            EnemyController enemy = col.gameObject.GetComponent<EnemyController>();

            if (enemy == null)
            {
                enemy = col.gameObject.GetComponentInParent<EnemyController>();
                if (enemy == null)
                    continue;
            }

            if (!enemys.Contains(enemy))
                enemys.Add(enemy);
        }
    }

    public void OnDamagedToEnemy()
    {
        foreach(EnemyController enemy in enemys)
        {
            if (enemy == null)
                continue;

            // temp
            enemy.Damaged(playerStatus.AttackPower);
        }

        enemys.Clear();
    }
    
}
