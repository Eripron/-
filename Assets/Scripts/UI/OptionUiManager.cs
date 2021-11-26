using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUiManager : Singleton<OptionUiManager>
{
    [SerializeField] GameObject optionWindow;

    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    SceneMover sm;

    bool state = false;

    void Start()
    {
        optionWindow.SetActive(state);

        // юс╫ц 
        bgmSlider.value = 1.0f;
        sfxSlider.value = 1.0f;

        SoundManager.Instance.ChangeBGMVolume(1.0f);
        SoundManager.Instance.ChangeSFXVolume(1.0f);

        sm = FindObjectOfType<SceneMover>();
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
