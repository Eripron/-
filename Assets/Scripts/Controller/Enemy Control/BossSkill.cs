using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSkill : PoolObject<BossSkill>
{

    Transform parent;
    [SerializeField] ParticleSystem skillEffect;
    [SerializeField] GameObject rangeImage;

    new CapsuleCollider collider;

    //[SerializeField] SpriteRenderer render; 


    Vector3 scale;
    float value = 1f;
    public void OnSkill()
    {
        if(collider == null)
            collider = GetComponent<CapsuleCollider>();

        collider.enabled = false;

        transform.parent = null;

        scale = new Vector3(value, value, value);
        transform.localScale = scale;

        StartCoroutine(OnSkillRange());
    }

    WaitForSeconds wait = new WaitForSeconds(0.1f);
    IEnumerator OnSkillRange()
    {
        rangeImage.SetActive(true);

        while (value < 10f)
        {
            value += 1f;
            scale = new Vector3(value, value, value);
            transform.localScale = scale;
            yield return wait;
        }

        value = 3f;
        scale = new Vector3(value, value, value);
        transform.localScale = scale;
        rangeImage.SetActive(false);

        collider.enabled = true;
        skillEffect.Play();

        yield return new WaitForSeconds(0.5f);
        collider.enabled = false;

        yield return new WaitForSeconds(2f);
        OnReturnForce();
    }

    public void SetParent(Transform _parent)
    {
        parent = _parent;
    }

}
