using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextUIManager : Singleton<DamageTextUIManager>
{
    [SerializeField] DamageText textPrefab;             // text prefab
    [SerializeField] int initCount;                     // ���� ���� ���� 

    [SerializeField] Color[] colors;

    Stack<DamageText> storage;                          // ����� 


    void Start()
    {
        storage = new Stack<DamageText>();

        for(int i=0; i<initCount; i++)
        {
            CreateText();
        }
    }

    // ���� 
    void CreateText()
    {
        DamageText text = Instantiate(textPrefab, transform);
        text.gameObject.SetActive(false);

        text.Return(ReturnText);

        storage.Push(text);
    }


    DamageText GetDamageText()
    {
        if (storage.Count <= 0)
            CreateText();

        return storage.Pop();
    }

    public void PlayDamageText(float damage, Vector3 pos, int colorNum)
    {
        DamageText dt = GetDamageText();
        dt.OnInit(damage, pos, colors[colorNum]);
    }

    public void ReturnText(DamageText text)
    {
        if (storage.Contains(text))
            return;

        storage.Push(text);
    }

}
