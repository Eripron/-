using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextUIManager : Singleton<DamageTextUIManager>
{
    [SerializeField] DamageText textPrefab;
    [SerializeField] int initCount;

    Stack<DamageText> storage;

    void Start()
    {
        storage = new Stack<DamageText>();

        for(int i=0; i<initCount; i++)
        {
            CreateText();
        }
    }

    // 생성 
    void CreateText()
    {
        DamageText text = Instantiate(textPrefab, transform);
        text.gameObject.SetActive(false);
        storage.Push(text);
    }

    // 가져오기 
    public DamageText GetDamageText()
    {
        if (storage.Count <= 0)
            CreateText();

        storage.Peek().gameObject.SetActive(true);
        return storage.Pop();
    }


}
