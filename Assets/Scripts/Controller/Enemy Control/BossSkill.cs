using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSkill : PoolObject<BossSkill>
{

    Transform parent;
    [SerializeField] ParticleSystem skillEffect;

    [SerializeField] SpriteRenderer render; 

    Color color = Color.red;

    public void OnSkill()
    {
        color.a = 0;
        render.color = color;

        StartCoroutine(OnSkillEffect());
    }

    IEnumerator OnSkillEffect()
    {
        while (color.a >= 0.8f)
        {
            Debug.Log($"{color.a}");
            color.a += 0.1f;
            render.color = color;
            yield return null;
        }

        skillEffect.Play();

        yield return new WaitForSeconds(0.5f);
        OnReturnForce();
    }

    public void SetParent(Transform _parent)
    {
        parent = _parent;
    }

}
