using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBound : MonoBehaviour
    , IPointerClickHandler
{
    CameraController cam;

    void Start()
    {
        cam = FindObjectOfType<CameraController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        cam.getMouseClick = true;
    }
}
