using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonUI : MonoBehaviour
{
    [SerializeField] Button optionButton;
    [SerializeField] Button closeGameButton;

    void Start()
    {
        optionButton.onClick.AddListener(OptionUiManager.Instance.SetActive);
        closeGameButton.onClick.AddListener(ExitGame);
    }

    public void ExitGame()
    {
        Debug.Log("game ����");
        Application.Quit();
    }

}
