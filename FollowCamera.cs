using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float turnSpeed;
    [SerializeField] float limitUpHeight;
    [SerializeField] float limitDownHeight;


    Camera cam;

    float minView = 5f;
    float maxView = 30f;
    [SerializeField] float viewInterval;

    Vector3 camPos;

    void Start()
    {
        cam = Camera.main;
        cam.fieldOfView = maxView;

        camPos = target.position + offset;
    }

    private void Update()
    {
        ChangeFieldOfView();
    }

    void LateUpdate()
    {
        float x = Input.GetAxis("Mouse X") * turnSpeed;
        float y = Input.GetAxis("Mouse Y") * turnSpeed;

        Quaternion rotateX = Quaternion.AngleAxis(x, Vector3.up);
        Quaternion rotateY = Quaternion.AngleAxis(y, Vector3.left);
             

        camPos = rotateX * rotateY * camPos;
        Vector3 finalPos = target.position + camPos;

        finalPos.y = Mathf.Clamp(finalPos.y, target.position.y - limitDownHeight, target.position.y + limitUpHeight);

        transform.position = finalPos;
        transform.LookAt(target);
    }

    void ChangeFieldOfView()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");

        if(wheel > 0f)
            cam.fieldOfView -= viewInterval;
        else if(wheel < 0f)
            cam.fieldOfView += viewInterval;

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minView, maxView);
    }
}
