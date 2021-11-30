using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] int slotNumber;
    [SerializeField] Text slotNumText;

    [SerializeField] Image itemImage;
    [SerializeField] Image timeMaskImage;

    [SerializeField] Item itemInSlot;


    void Start()
    {
        Init();
    }

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
        

    }

    public void OnDrag(PointerEventData eventData)
    {
        itemImage.transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.transform.position = transform.position;



    }
}
