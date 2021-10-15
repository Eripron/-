using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventSender : MonoBehaviour
{
    public UnityEvent<string> OnSendEvent;
    public UnityEvent OnSendEvent2;
    public UnityEvent OnSendEvent3;

    public void OnEventSend(string str)
    {
        // ���� ���� �˸��� �Լ� 
        OnSendEvent?.Invoke(str);
    }

    public void AnimEvent()
    {
        // ������ ������ ������ �ҷ��� ���� ������ ���� ���� ������ �Ǵ��ϴ� �Լ� 
        OnSendEvent2?.Invoke();
    }

    public void OnEndDashEvent()
    {
        OnSendEvent3?.Invoke();
    }
}
