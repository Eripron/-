using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWindow : MonoBehaviour
{
    [SerializeField] Button optionButton;


    void Start()
    {
        optionButton.onClick.AddListener(OptionUiManager.Instance.SetActive);     
    }
}
