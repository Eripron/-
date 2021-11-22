using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInformation : MonoBehaviour
{
    DungeonMapUI mapUi;
    CameraController cam;

    void Start()
    {
        mapUi = DungeonMapUI.Instance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (cam == null)
                cam = FindObjectOfType<CameraController>();

            cam.OnMouseAble();

            if (mapUi != null)
                mapUi.OnOpenDungeonMapUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (mapUi != null)
                mapUi.OnOffDungeonMapUI();
        }
    }

}
