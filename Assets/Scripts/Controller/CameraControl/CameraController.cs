using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float turnSpeed;
    [SerializeField] float limitUpHeight;
    [SerializeField] float limitDownHeight;
    [SerializeField] float boundaryBottom;


    Camera cam;

    float minView = 10f;
    float maxView = 45f;

    float bottomY;

    [SerializeField] float viewInterval;

    Vector3 camPos;

    bool isControl = true;

    void Start()
    {
        cam = Camera.main;
        cam.fieldOfView = maxView;

        camPos = offset;
    }

    private void Update()
    {
        if (!isControl)
            return;

        ChangeFieldOfView();

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            bottomY = hit.point.y;
        }
    }

    void LateUpdate()
    {
        if (!isControl)
            return;

        float x = Input.GetAxis("Mouse X") * turnSpeed;
        float y = Input.GetAxis("Mouse Y") * turnSpeed;

        Quaternion rotateX = Quaternion.AngleAxis(x, Vector3.up);
        Quaternion rotateY = Quaternion.AngleAxis(y, Vector3.left);


        camPos = rotateX * rotateY * camPos;

        Vector3 finalPos = camPos + target.position;

        finalPos.y = Mathf.Clamp(finalPos.y, target.position.y - limitDownHeight, target.position.y + limitUpHeight);

        transform.position = ClampBoundary(finalPos);
        transform.LookAt(target);
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomY + boundaryBottom, limitUpHeight);
        return vector;
    }

    public void SetCameraControlState(bool state)
    {
        isControl = state;
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
