using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] int slotNumber;
    [SerializeField] Text slotNumText;

    [SerializeField] Image itemImage;
    [SerializeField] Image timeMaskImage;

    [SerializeField] Item itemInSlot;


    QuickSlotUIManager slotManager;

    void Start()
    {
        Init();
    }

    public Item ItemInSlot => itemInSlot;


    void Init()
    {
        slotNumText.text = slotNumber.ToString();

        if (itemInSlot != null)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = itemInSlot.ItemImage;
        }
        else
            itemImage.gameObject.SetActive(false);

        timeMaskImage.gameObject.SetActive(false);

        if(slotManager == null)
            slotManager = GetComponentInParent<QuickSlotUIManager>();
    }


    public void PressedQuickSlot()
    {
        if (itemInSlot == null)
        {
            Debug.Log($"Null Item in Slot");
            return;
        }

        itemInSlot.PressQuickSlot();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        /* 
         item이 slot에 있다면 현재 드래그 시작 
         slot number를 저장한다.
        */
        if (itemInSlot != null)
        {
            Debug.Log("드래그 시작");
            slotManager.SetCurSlotNum(slotNumber);
        }
        else
        {
            slotManager.SetCurSlotNum(-1);
        }

    }


    public void OnDrag(PointerEventData eventData)
    {
        if (itemInSlot != null)
        {
            Debug.Log("드래그 중");
            itemImage.transform.position = eventData.position;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.transform.position = transform.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (slotManager.IsDiffrentSlot(slotNumber) && slotManager.IsEmptySlot(slotNumber))
        {
            Debug.Log("move item slot to slot");
            slotManager.MoveSlotToSlot(itemInSlot);
        }
    }

    public void ResetQuickSlot()
    {
        itemInSlot = null;
        Init();
    }
    
    public void SetQuickSlot(Item _item)
    {
        Debug.Log("item seting in slot ");
        itemInSlot = _item;
        Init();
    }
}
