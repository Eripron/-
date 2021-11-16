using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionInfo : MonoBehaviour
{

    // temp
    // 현재 이 지역을 비추는 미니맵 카메라의 뷰를 설정할 값을 가지고 있다.
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
