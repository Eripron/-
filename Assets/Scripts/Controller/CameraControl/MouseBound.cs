using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBound : MonoBehaviour
    , IPointerClickHandler
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        cam.GetComponent<CameraController>().getMouseClick = true;
    }
}
