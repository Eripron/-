using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotUIManager : MonoBehaviour
{
    [SerializeField] QuickSlot[] quickSlots;

    KeyCode[] quickCode = new KeyCode[10];
    private void Start()
    {
        int index = 0;
        for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; key++)
            quickCode[index++] = key;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {

        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
    }

    private void OnKeyDown(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.Alpha1:
                break;
        }
    }



}
