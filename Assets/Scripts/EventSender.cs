using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventSender : MonoBehaviour
{
    [SerializeField] GameObject target;

    public UnityEvent OnSendEvent;
    public UnityEvent OnSendEvent2;
    public UnityEvent OnSendEvent3;
    public UnityEvent OnSendEvent4;
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

    public void ChangeTag(string tagName)
    {
        Debug.Log(tagName);
        target.tag = tagName;
    }

}
