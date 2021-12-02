using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemImage;

    [SerializeField] int coolTime;
    [SerializeField] int amount;



    QuickSlot connectedQuickSlot;

    public string Itemame => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemImage => itemImage;
    public int CoolTime => coolTime;
    public int Amount
    {
        get
        {
            return amount;
        }
        set
        {
        }
    }

    public bool IsConnectedToSlot()
    {
        return connectedQuickSlot != null;
    }

    public void OnSetSlotDataToItem(QuickSlot slotData)
    {
        connectedQuickSlot = slotData;
    }
    
    public void PressQuickSlot()
    {
        UsePortion();
    }

    public bool isCanUse = true;

    private void UsePortion()
    {
        if (!isCanUse || amount <= 0)
        {
            return;
        }

        Debug.Log("아이템 사용 시작");
        isCanUse = false;
        amount--;

        if (amount <= 0)
        {
            connectedQuickSlot.ResetQuickSlot();
            return;
        }

        // quick slot update
        connectedQuickSlot.UpdateQuickSlot();
        connectedQuickSlot.OnCoolTime(CoolTime);
    }

    public void OnCanUseItem()
    {
        isCanUse = true;
    }

}
