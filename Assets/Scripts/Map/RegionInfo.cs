using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionInfo : MonoBehaviour
{

    // 현재 이 지역을 비추는 미니맵 카메라의 뷰를 설정할 값을 가지고 있다.
    [SerializeField] float minimapCameraViewValue;

    public float CameraViewValue => minimapCameraViewValue;


    public void OnOpenRegion()
    {
        this.gameObject.SetActive(true);
    }
    public void OnCloseRegion()
    {
        this.gameObject.SetActive(false);
    }
}
