using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUiManager : MonoBehaviour
{
    [SerializeField] GameObject optionWindow;

    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    bool state = false;

    void Start()
    {
        optionWindow.SetActive(state);

        // юс╫ц 
        bgmSlider.value = 1.0f;
        sfxSlider.value = 1.0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
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

}
