using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item Data", menuName = "Create Item/New Item Data")]
public class Item : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemImage;

    [SerializeField] int waitTime;
    [SerializeField] int amount;


    int count = -1; 

    public string Itemame => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemImage => itemImage;
    public int WaitTime => waitTime;


    public void PressQuickSlot()
    {
        Debug.Log($"Use {itemName}");
        UsePortion();
    }


    bool isCanUse = true;


    private void UsePortion()
    {
        Debug.Log(count);

        if (count == -1)
            count = amount;

        if (!isCanUse || count <= 0)
            return;

        //isCanUse = false;
        count--;

        Debug.Log("hp È¸º¹");
    }


}
