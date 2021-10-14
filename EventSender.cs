using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSender : MonoBehaviour
{
    public UnityEvent<string> OnSendEvent;

    public void OnEventSend(string str)
    {
        OnSendEvent?.Invoke(str);
    }

}
