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

    [SerializeField] CameraShake mainCam;
    [SerializeField] Transform effectPivot;

    PlayerStatus playerStatus;


    // �˻��� �� ����
    List<IDamaged> enemys = new List<IDamaged>();

    void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();
        mainCam = FindObjectOfType<CameraShake>();
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
            {
                if(HitEffectManager.Instance != null)
                    HitEffectManager.Instance.OnPlayerHitEffect(effectPivot);

                enemys.Add(enemy);
            }
        }
    }

    public void OnDamagedToEnemy()
    {
        foreach(IDamaged enemy in enemys)
        {
            if (enemy == null)
                continue;

            mainCam.OnShake();

            // ���ݷ� ���� 
            int ranPower = Random.Range(playerStatus.AttackPower - playerStatus.AttackPower / 3, playerStatus.AttackPower + playerStatus.AttackPower / 3);
            enemy.Damaged(ranPower);

            DamageTextUIManager.Instance.GetDamageText().OnInit(ranPower, enemy.GetEnemyPos());
            playerStatus.AddSp(ranPower / 8);
        }

        enemys.Clear();
    }
    
}
