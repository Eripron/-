using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotUIManager : Singleton<QuickSlotUIManager>
{
    // �� ���� ��ü�� ������ �ְ� �ش� key �Է��� �޾Ƽ� �ش� ���Կ� �˸���.
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


    // quick slot Ŭ���ϸ� manager���� ���� slot num����.
    public void SetCurSlotNum(int slotNum)
    {
        currentSlotNum = slotNum;
    }


    // quick slot �巡�� ���� ������ ���� �����ΰ� �ƴѰ�.
    public bool IsDiffrentSlot(int slotNum)
    {
        // -1�� item�� ���� �������� ���� �Դٴ� ���� ǥ�� 
        if (currentSlotNum == -1)
            return false;

        nextSlotNum = slotNum;
        Debug.Log($"{slotNum}�̶� ��");
        return currentSlotNum != nextSlotNum;
    }

    // Ư�� slot�� item�� �ִ°� ?
    public bool IsEmptySlot(int slotNum)
    {
        Debug.Log($"{slotNum} �����");
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
