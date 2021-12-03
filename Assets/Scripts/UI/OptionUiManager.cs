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

    //
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] Dropdown resolutionDropdown;
    Resolution[] resolutions;

    new void Awake()
    {
        base.Awake();

        optionWindow.SetActive(state);

        bgmSlider.value = 1.0f;
        sfxSlider.value = 1.0f;

        soundManager.ChangeBGMVolume(1.0f);
        soundManager.ChangeSFXVolume(1.0f);

        sm.AddCloseWindowFun(OnOffOptionWindow);

        // ----
        resolutions = Screen.resolutions;

        int currentResolutionIndex = 0;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetFullscreen(fullScreenToggle.isOn);
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


    public void SetFullscreen(bool isFullscreen)
    {
        Debug.Log($"전체화면 : {isFullscreen}");
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex)
    {
        Debug.Log($"index  :  {resolutionIndex} 화면 change ");
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
