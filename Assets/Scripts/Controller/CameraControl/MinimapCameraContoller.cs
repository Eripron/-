using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraContoller : MonoBehaviour
{
    [SerializeField] float offsetY;
    [SerializeField] Transform playerIcon;

    Transform _transform;
    Movement player;

    Camera minimapCam;

    void Start()
    {
        _transform = this.transform;
        player = Movement.Instance;

        minimapCam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        float x = player.transform.position.x;
        float z = player.transform.position.z;
        _transform.position = new Vector3(x, offsetY, z);
    }

    public void OnSetMinimapCamera(float cameraSize, Vector3 iconSize)
    {
        minimapCam.orthographicSize = cameraSize;
        playerIcon.localScale = iconSize;
    }



}
