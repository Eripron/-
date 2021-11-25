using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUiManager : MonoBehaviour
{
    [SerializeField] GameObject optionWindow;


    bool state = false;

    void Start()
    {
        optionWindow.SetActive(state);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            state = !state;
            optionWindow.SetActive(state);
        }
    }

}
