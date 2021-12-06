using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillContoller : PoolManager<BossSkillContoller, BossSkill>
{
    [SerializeField] EnemyStatus boss;

    public void OnSkill(Vector3 instantPos)
    {

        BossSkill skill = GetPool();

        skill.GetComponent<EnemyAttackAble>().SetEnemyStatus(boss);
        skill.SetParent(this.transform);

        skill.transform.position = instantPos;

        skill.OnSkill();
    }



}
