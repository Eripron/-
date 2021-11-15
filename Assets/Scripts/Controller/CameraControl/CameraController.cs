using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] int maxDistance;

    [SerializeField] float turnSpeed;
    [SerializeField] float viewInterval;

    [SerializeField] float boundaryBottom;
    [SerializeField] float limitUpHeight;
    [SerializeField] float limitDownHeight;


    Camera cam;

    float cameraDistance;

    float minView = 10f;
    float maxView = 45f;
    float bottomY;

    Vector3 camPos;

    bool isControl = true;

    bool mouseLocked = false;
    public bool getMouseClick = false;

    void Start()
    {
        cam = Camera.main;
        cam.fieldOfView = maxView;

        // 카메라와 플레이어까지의 거리 
        SetCameraDistance(maxDistance);
    }

    public bool CursorLockState()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }

    public void OnMouseAble()
    {
        // 외부에서 호출하는 마우스 on 함수 
        mouseLocked = false;
        Cursor.lockState = CursorLockMode.None;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && mouseLocked == true)
        {
            mouseLocked = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (mouseLocked == false && getMouseClick == true)
        {
            getMouseClick = false;
            mouseLocked = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        ChangeFieldOfView();

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            bottomY = hit.point.y;
        }
    }


    Vector2 input;

    void LateUpdate()
    {
        // new rotate

        input.x = Input.GetAxis("Mouse X") * turnSpeed;
        input.y = Input.GetAxis("Mouse Y") * turnSpeed;

        if(input.magnitude != 0.0f)
        {
            Quaternion rotation = target.rotation;
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x + input.y, rotation.eulerAngles.y + input.x, rotation.eulerAngles.z);
            target.rotation = rotation;
        }

        transform.LookAt(target);

    }

    // 원래 회전방식 
    void OriginRotate()
    {
        if (isControl && mouseLocked)
        {
            float x = Input.GetAxis("Mouse X") * turnSpeed;
            float y = Input.GetAxis("Mouse Y") * turnSpeed;

            Quaternion rotateX = Quaternion.AngleAxis(x, Vector3.up);
            Quaternion rotateY = Quaternion.AngleAxis(y, Vector3.left);

            camPos = rotateX * rotateY * camPos;
        }


        Vector3 finalPos = camPos + target.position;

        Vector3 dir = (finalPos - target.position).normalized;
        RaycastHit hitted;
        if (Physics.Raycast(target.position, dir, out hitted, cameraDistance))
        {
            if (hitted.transform.gameObject.layer == (int)LAYER.LAYER_GROUND)
                finalPos = hitted.point - (dir * 1.5f);
        }

        finalPos.y = Mathf.Clamp(finalPos.y, target.position.y - limitDownHeight, target.position.y + limitUpHeight);

        transform.position = ClampBoundary(finalPos);
        transform.LookAt(target);
    }


    void SetCameraDistance(float _distance)
    {
        cameraDistance = Mathf.Abs(_distance);
        camPos = new Vector3(0f, 0f, cameraDistance);
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
