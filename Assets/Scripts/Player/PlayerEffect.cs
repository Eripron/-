using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : PoolObject<PlayerEffect>
{
    ParticleSystem ps;
    PlayerEffectManager parent;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps == null)
            return;

        if (!ps.isPlaying)
        {
            SetParent(parent);
            OnReturnForce();
        }
    }

    public void SetParent(PlayerEffectManager _parent)
    {
        parent = _parent;
    }
}
