using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionInfo : MonoBehaviour
{

    // ���� �� ������ ���ߴ� �̴ϸ� ī�޶��� �並 ������ ���� ������ �ִ�.
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
