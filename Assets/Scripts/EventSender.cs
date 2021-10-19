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
        // 가드 끝을 알리는 함수 
        OnSendEvent?.Invoke();
    }

    public void OnEventSend2()
    {
        // 공격이 끝나는 지점에 불려서 다음 공격을 할지 할지 안할지 판단하는 함수 
        OnSendEvent2?.Invoke();
    }

    public void OnEventSend3()
    {
        OnSendEvent3?.Invoke();
    }
}
