using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventSender : MonoBehaviour
{
    // 공격 검 효과 
    [SerializeField] GameObject attackEffect;
    [SerializeField] GameObject footEffect;

    [SerializeField] GameObject target;
    [SerializeField] PlayerAttackAble attackAble;


    public UnityEvent OnSendEvent;
    public UnityEvent OnSendEvent2;
    public UnityEvent OnSendEvent3;
    public UnityEvent OnSendEvent4;

    bool isCheck;

    public void OnEventSend()
    {
        OnSendEvent?.Invoke();
    }
    public void OnEventSend2()
    {
        OnSendEvent2?.Invoke();
    }
    public void OnEventSend3()
    {
        OnSendEvent3?.Invoke();
    }
    public void OnEventSend4()
    {
        OnSendEvent4?.Invoke();
    }

    void Update()
    {
        if (isCheck)
        {
            attackAble.OnCheckEnemyInAttackArea();
        }
    }

    public void ChangeTag(string tagName)
    {
        Debug.Log(tagName);
        target.tag = tagName;
    }
    public void GetUpPlayer()
    {
        Movement player = target.GetComponent<Movement>();
        if (player == null)
            return;

        Debug.Log("call reset");
        player.AllReset();
    }
    public void OnStartCheckEnemyInAttack()
    {
        isCheck = true;

        // 검신 효과 
        Debug.Log("on");
        attackEffect.SetActive(true);
    }
    public void OnEndCheckEnemyInAttack()
    {
        isCheck = false;
        attackAble.OnDamagedToEnemy();

        OnOffAttackEffect();
    }

    public void OnOffAttackEffect()
    {
        attackEffect.SetActive(false);
    }

    public void OnFootEffect()
    {
        if(footEffect.activeSelf == true)
            footEffect.SetActive(false);

        footEffect.SetActive(true);
    }

}
