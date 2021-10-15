using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSender : MonoBehaviour
{
    public UnityEvent<string> OnSendEvent;

    public UnityEvent OnSendEvent2;

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
}
