using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectManager : PoolManager<HitEffectManager, HitEffect>
{
    public void OnPlayerHitEffect(Transform pos)
    {
        HitEffect hit = GetPool();
        hit.transform.position = pos.position;
    }
}
