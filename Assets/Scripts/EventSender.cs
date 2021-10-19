using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventSender : MonoBehaviour
{
    public UnityEvent OnSendEvent;
    public UnityEvent OnSendEvent2;
    public UnityEvent OnSendEvent3;

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
}
