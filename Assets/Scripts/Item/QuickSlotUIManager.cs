using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuickSlotUIManager : MonoBehaviour
{
    // 퀵 슬롯 전체를 가지고 있고 해당 key 입력을 받아서 해당 슬롯에 알린다.
    [SerializeField] QuickSlot[] quickSlots;


    int currentSlotNum;
    int nextSlotNum;


    void Update()
    {
        #region Input_Key
        if (Input.GetKeyDown(KeyCode.Alpha1))
            quickSlots[1].PressedQuickSlot();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            quickSlots[2].PressedQuickSlot();
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            quickSlots[3].PressedQuickSlot();
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            quickSlots[4].PressedQuickSlot();
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            quickSlots[5].PressedQuickSlot();
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            quickSlots[6].PressedQuickSlot();
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            quickSlots[7].PressedQuickSlot();
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            quickSlots[8].PressedQuickSlot();
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            quickSlots[9].PressedQuickSlot();
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            quickSlots[0].PressedQuickSlot();
        #endregion
    }

    // quick slot 클릭하면 manager에게 현재 slot num저장.
    public void SetCurSlotNum(int slotNum)
    {
        currentSlotNum = slotNum;
    }


    // quick slot 드래그 끝난 지점이 같은 슬롯인가 아닌가.
    public bool IsDiffrentSlot(int slotNum)
    {
        // -1은 item이 없는 슬롯으로 부터 왔다는 것을 표시 
        if (currentSlotNum == -1)
            return false;

        nextSlotNum = slotNum;
        return currentSlotNum != nextSlotNum;
    }

    // 특정 slot에 item이 있는가 ?
    public bool IsEmptySlot(int slotNum)
    {
        return quickSlots[slotNum].ItemInSlot == null;
    }

    public void MoveSlotToSlot(Item item)
    {
        Debug.Log(item);
        Debug.Log($"{currentSlotNum}, {nextSlotNum}");

        quickSlots[nextSlotNum].SetQuickSlot(item);
        quickSlots[currentSlotNum].ResetQuickSlot();
    }

}
