using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemImage;

    [SerializeField] int waitTime;
    [SerializeField] int amount;


    public string Itemame => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemImage => itemImage;
    public int WaitTime => waitTime;
    public int Amount { get; set; }

    
    public void PressQuickSlot()
    {
        Debug.Log($"Use {itemName}");
        UsePortion();
    }

    bool isCanUse = true;

    private void UsePortion()
    {
        if (!isCanUse || amount <= 0)
        {
            Debug.Log("쿨타임......");
            return;
        }

        Debug.Log("아이템 사용 시작");
        isCanUse = false;
        amount--;

        StartCoroutine(CoolDownCoroutine());
    }

    WaitForSeconds oneSecond = new WaitForSeconds(1.0f);

    IEnumerator CoolDownCoroutine()
    {
        int count = WaitTime;

        while (count > 0)
        {
            count--;
            Debug.Log($"remain {count} sec");
            yield return oneSecond;
        }

        Debug.Log("쿨타임 끝 !!!");
        isCanUse = true;
    }


}
