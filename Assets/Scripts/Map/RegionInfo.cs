using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionInfo : MonoBehaviour
{

    // temp
    // ���� �� ������ ���ߴ� �̴ϸ� ī�޶��� �並 ������ ���� ������ �ִ�.
    [SerializeField] float miniMapCameraSize;
    [SerializeField] Vector3 playerIconSize;

    MinimapCameraContoller minimapCam;

    void Awake()
    {
        minimapCam = FindObjectOfType<MinimapCameraContoller>();     
    }

    public void OnOpenRegion()
    {
        this.gameObject.SetActive(true);
    }
    public void OnCloseRegion()
    {
        this.gameObject.SetActive(false);
    }

    public void OnSetMinimapCamera()
    {
        minimapCam.OnSetMinimapCamera(miniMapCameraSize, playerIconSize);
    }

}
