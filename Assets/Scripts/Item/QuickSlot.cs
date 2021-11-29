using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuickSlot : MonoBehaviour
{
    /*
     
     */

    [SerializeField] int slotNumber;
    [SerializeField] Text slotNumText;

    [SerializeField] Image itemImage;
    [SerializeField] Image timeMaskImage;

    void Start()
    {
        InitQuickSlot();
    }

    void InitQuickSlot()
    {
        slotNumText.text = slotNumber.ToString();
    }
    
}
