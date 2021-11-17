using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectManager : PoolManager<HitEffectManager, HitEffect>
{

    public void OnPlayerHitEffect(Transform pos)
    {
        Debug.Log("Pos");
        HitEffect hit = GetPool();
        hit.transform.position = pos.position;
    }
}
