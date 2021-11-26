using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBound : MonoBehaviour
    , IPointerClickHandler
{
    CameraController cam;
    OptionUiManager oup;

    void Start()
    {
        cam = FindObjectOfType<CameraController>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (oup == null)
            oup = OptionUiManager.Instance;

        cam.getMouseClick = true;

        if(oup.gameObject.activeSelf)
            oup.OnOffOptionWindow();
    }
}
