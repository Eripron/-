using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonUI : MonoBehaviour
{
    [SerializeField] Button optionButton;
    [SerializeField] Button closeGameButton;

    [SerializeField] BGM _bgm;

    void Start()
    {
        optionButton.onClick.AddListener(OptionUiManager.Instance.SetActive);
        closeGameButton.onClick.AddListener(ExitGame);

        SoundManager.Instance.PlayBGM(_bgm.ToString(), true);
    }

    public void ExitGame()
    {
        Debug.Log("game Á¾·á");
        Application.Quit();
    }

}
