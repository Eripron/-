using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    private static DontDestroyManager instance = null;

    List<string> notDesList; 

    void Awake()
    {
        Debug.Log("Awake");
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            notDesList = new List<string>();
        }
        else
            Destroy(gameObject);

    }


    public void AddDontDestroyObject(DontDestory dd)
    {
        if(!notDesList.Contains(dd.gameObject.name))
        {
            Debug.Log("요청 승인");
            notDesList.Add(dd.ddName);
            dd.OnSetParent(transform);
        }
        else
        {
            dd.OnBreakGo();
            Debug.Log("요청 불허");
        }

    }

}
