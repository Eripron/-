using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillContoller : PoolManager<BossSkillContoller, BossSkill>
{

    public void OnSkill(Vector3 instantPos)
    {
        BossSkill skill = GetPool();

        skill.SetParent(this.transform);
        skill.transform.parent = null;
        skill.transform.position = instantPos;

        skill.OnSkill();
    }



}
