using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraContoller : MonoBehaviour
{
    [SerializeField] float offsetY;


    Transform _transform;
    Movement player;

    void Start()
    {
        _transform = this.transform;
        player = Movement.Instance;
    }

    void LateUpdate()
    {
        float x = player.transform.position.x;
        float z = player.transform.position.z;
        _transform.position = new Vector3(x, offsetY, z);
    }


}
