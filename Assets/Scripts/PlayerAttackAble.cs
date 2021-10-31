using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAble : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] float attackRadius;
    [SerializeField] Transform startAttackPivot;
    [SerializeField] Transform endAttackPivot;
    [SerializeField] LayerMask enemyLayer;

    PlayerStatus playerStatus;

    // 검색한 적 저장
    List<IDamaged> enemys = new List<IDamaged>();

    void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();     
    }

    public void OnCheckEnemyInAttackArea()
    {
        Collider[] colliders = Physics.OverlapCapsule(startAttackPivot.position, endAttackPivot.position, attackRadius, enemyLayer);

        foreach(Collider col in colliders)
        {
            IDamaged enemy = col.gameObject.GetComponent<IDamaged>();

            if (enemy == null)
            {
                enemy = col.gameObject.GetComponentInParent<IDamaged>();
                if (enemy == null)
                    continue;
            }

            if (!enemys.Contains(enemy))
                enemys.Add(enemy);
        }
    }

    public void OnDamagedToEnemy()
    {
        foreach(IDamaged enemy in enemys)
        {
            if (enemy == null)
                continue;

            enemy.Damaged(playerStatus.AttackPower);
            playerStatus.AddSp(4);
        }

        enemys.Clear();
    }
    
}
