using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealTimeUI : MonoBehaviour
{
    [SerializeField] Text timeText;

    WaitForSeconds wait = new WaitForSeconds(0.01f);

    void Start()
    {
        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            var today = System.DateTime.Now;
            timeText.text = today.ToString("h : mm tt");
            yield return wait;
        }
    }

}
