using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : PoolObject<HitEffect>
{
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps == null)
            return;

        if(!ps.isPlaying)
        {
            OnReturnForce();
        }
    }


}
