using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] int slotNumber;
    [SerializeField] Text slotNumText;
    [SerializeField] Text amountText;
    [SerializeField] Text coolTimeText;

    [SerializeField] Image itemImage;
    [SerializeField] Image coolTimeImage;


    [SerializeField] Item itemInSlot;

    [SerializeField] Transform selectedParent;


    QuickSlotUIManager slotManager;
    public Item ItemInSlot => itemInSlot;


    void Start()
    {
        Init();
    }


    void Init()
    {
        if(slotManager == null)
            slotManager = GetComponentInParent<QuickSlotUIManager>();

        slotNumText.text = slotNumber.ToString();
        coolTimeImage.fillAmount = 0f;

        UpdateQuickSlot();
    }


    public void UpdateQuickSlot()
    {
        // slot�� �������� �ִ°�?

        // ���ٸ� 
        if (itemInSlot == null)
        {
            // ���� �̹����� ����.
            itemImage.gameObject.SetActive(false);
        }
        // �ִٸ� 
        else if (itemInSlot != null)
        {
            // �ִµ� ������ ����Ѱ�?
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = itemInSlot.ItemImage;
            amountText.text = itemInSlot.Amount.ToString();
        }
        
        // ���� ���� 
        // timeMaskImage.gameObject.SetActive(false);
    }


    public void PressedQuickSlot()
    {
        if (itemInSlot == null)
        {
            Debug.Log($"Null Item in Slot");
            return;
        }

        itemInSlot.OnSetSlotDataToItem(this);
        itemInSlot.PressQuickSlot();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        /* 
         item�� slot�� �ִٸ� ���� �巡�� ���� 
         slot number�� �����Ѵ�.
        */

        if (itemInSlot != null)
        {
            Debug.Log("�巡�� ����");
            slotManager.SetCurSlotNum(slotNumber);

            // �巡�� ���� �ϸ� �θ� ���� 
            itemImage.transform.SetParent(selectedParent);
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
            Debug.Log("�巡�� ��");
            itemImage.transform.position = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("�巡�� ��");
        itemImage.transform.SetParent(this.transform);
        itemImage.transform.SetSiblingIndex(0);

        itemImage.transform.position = transform.position;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"drop {slotNumber}");
        if (slotManager.IsDiffrentSlot(slotNumber) && slotManager.IsEmptySlot(slotNumber))
        {
            Debug.Log("move item slot to slot");
            slotManager.MoveSlotToSlot();
        }
    }


    public void SetQuickSlot(Item _item)
    {
        Debug.Log("item seting in slot ");
        itemInSlot = _item;
        UpdateQuickSlot();
    }
    public void ResetQuickSlot()
    {
        itemInSlot = null;
        UpdateQuickSlot();
    }



    int CoolTime;
    int curCoolTime;
    float lerpAmount;

    public void OnCoolTime(int totalCoolTime)
    {
        if (totalCoolTime <= 0)
        {
            if (itemInSlot != null)
                itemInSlot.OnCanUseItem();
            return;
        }

        CoolTime = totalCoolTime;
        curCoolTime = totalCoolTime;

        coolTimeImage.fillAmount = 1.0f;
        coolTimeText.text = curCoolTime.ToString();

        StartCoroutine(CoolTimeCount());
    }

    WaitForSeconds oneSecond = new WaitForSeconds(1.0f);

    IEnumerator CoolTimeImage()
    {
        while(coolTimeImage.fillAmount > 0f)
        {
            coolTimeImage.fillAmount = Mathf.Lerp(coolTimeImage.fillAmount, (float)curCoolTime/CoolTime, Time.deltaTime * 1.5f);
            yield return null;
        }

        yield break;
    }


    IEnumerator CoolTimeCount()
    {
        while (curCoolTime > 0)
        {
            curCoolTime -= 1;
            StartCoroutine(CoolTimeImage());

            yield return oneSecond;

            coolTimeText.text = curCoolTime.ToString();
        }

        coolTimeText.text = string.Empty;
        CoolTime = 0;
        curCoolTime = 0;

        if (itemInSlot != null)
            itemInSlot.OnCanUseItem();
    }


}
