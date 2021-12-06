using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : PoolManager<PlayerEffectManager, PlayerEffect>
{
    Transform _transform;

    public void OnFootEffect()
    {
        if (_transform == null)
            _transform = this.transform;

        PlayerEffect effect = GetPool();

        effect.SetParent(this);
        effect.transform.parent = null;
        effect.transform.position = _transform.position;
    }
}
