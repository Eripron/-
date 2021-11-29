using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUiManager : Singleton<OptionUiManager>
{
    [SerializeField] GameObject optionWindow;

    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] SoundManager soundManager;
    [SerializeField] SceneMover sm;

    bool state = false;

    new void Awake()
    {
        base.Awake();

        optionWindow.SetActive(state);

        bgmSlider.value = 1.0f;
        sfxSlider.value = 1.0f;

        soundManager.ChangeBGMVolume(1.0f);
        soundManager.ChangeSFXVolume(1.0f);

        sm.AddCloseWindowFun(OnOffOptionWindow);
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && !sm.CurSceneName().Equals(SceneMover.SCENE.Menu.ToString()))
        {
            SetActive();
        }
    }


    public void SetActive()
    {
        state = !state;
        optionWindow.SetActive(state);

        if(state)
        {
            CameraController cam = Camera.main.GetComponent<CameraController>();
            if (cam != null)
                cam.OnMouseAble();
        }
    }
    public void OnOffOptionWindow()
    {
        state = false;
        optionWindow.SetActive(state);
    }



}
