using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour
{
    static int count = 0;

    [HideInInspector] public string ddName;
    int num = 0;

    private void Awake()
    {
        num = ++count;
        ddName = gameObject.name;
    }

    private void Start()
    {
        DontDestroyManager ddm = FindObjectOfType<DontDestroyManager>();
        if(ddm != null)
        {
            Debug.Log($"{num}������ {gameObject.name}�� DDM���� ��û");
            ddm.AddDontDestroyObject(this);
        }
    }

    public void OnSetParent(Transform parent)
    {
        Debug.Log("�θ� ����");
        transform.SetParent(parent);
    }


    public void OnBreakGo()
    {
        DestroyImmediate(gameObject);
        Debug.Log($"{num}���� ����");
    }
}
