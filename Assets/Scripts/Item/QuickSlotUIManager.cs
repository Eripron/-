using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuickSlotUIManager : MonoBehaviour
{
    // �� ���� ��ü�� ������ �ְ� �ش� key �Է��� �޾Ƽ� �ش� ���Կ� �˸���.
    [SerializeField] QuickSlot[] quickSlots;


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

}
