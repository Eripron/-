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
            Debug.Log($"{num}번쨰인 {gameObject.name}이 DDM에게 요청");
            ddm.AddDontDestroyObject(this);
        }
    }

    public void OnSetParent(Transform parent)
    {
        Debug.Log("부모 설정");
        transform.SetParent(parent);
    }


    public void OnBreakGo()
    {
        DestroyImmediate(gameObject);
        Debug.Log($"{num}번쨰 삭제");
    }
}
