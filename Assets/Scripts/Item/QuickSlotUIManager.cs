using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotUIManager : Singleton<QuickSlotUIManager>
{
    // 퀵 슬롯 전체를 가지고 있고 해당 key 입력을 받아서 해당 슬롯에 알린다.
    [SerializeField] QuickSlot[] quickSlots;

    [SerializeField] RectTransform desUI;
    [SerializeField] Text desNameText;
    [SerializeField] Text desText;

    Movement connectedPlayer;

    int currentSlotNum;
    int nextSlotNum;

    private new void Awake()
    {
        base.Awake();
        OffQuickSlotUI();
    }

    public void OnQuickSlotUI()
    {
        Debug.Log("on quick slot");
        gameObject.SetActive(true);
    }
    private void OffQuickSlotUI()
    {
        gameObject.SetActive(false);
    }

    SceneMover.SCENE _scene;

    void Update()
    {
        _scene = SceneMover.Instance.CurSceneEnum();
        if (_scene == SceneMover.SCENE.Town)
        {
            Debug.Log("can't use quick slot ui ");
            return;
        }
        else if(_scene == SceneMover.SCENE.Menu)
        {
            OffQuickSlotUI();
            return;
        }

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
        Debug.Log($"{slotNum}이랑 비교");
        return currentSlotNum != nextSlotNum;
    }

    // 특정 slot에 item이 있는가 ?
    public bool IsEmptySlot(int slotNum)
    {
        Debug.Log($"{slotNum} 비었다");
        return quickSlots[slotNum].ItemInSlot == null;
    }


    public void MoveSlotToSlot()
    {
        Debug.Log($"{currentSlotNum}, {nextSlotNum}");

        Item moveItem = quickSlots[currentSlotNum].ItemInSlot;

        quickSlots[currentSlotNum].ResetQuickSlot();
        quickSlots[nextSlotNum].SetQuickSlot(moveItem);
    }

    Vector2 newPos;

    public void OnDescription(int slotNum, Vector2 position)
    {
        newPos = (Vector2.up * 100f) + position;
        desUI.transform.position = newPos;

        desNameText.text = quickSlots[slotNum].ItemInSlot.ItemName;
        desText.text = quickSlots[slotNum].ItemInSlot.ItemDescription;

        desUI.gameObject.SetActive(true);
    }

    public void OffDescription()
    {
        desUI.gameObject.SetActive(false);
    }
}
