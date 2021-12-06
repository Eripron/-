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

    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
    }

    Vector3 scale;
    float value = 1f;
    public void OnSkill()
    {
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

        skillEffect.Play();
        collider.enabled = true;

        yield return new WaitUntil(() => skillEffect.isPlaying == false);
        collider.enabled = false;

        yield return new WaitForSeconds(2f);
        OnReturnForce();
    }

    public void SetParent(Transform _parent)
    {
        parent = _parent;
    }

}
