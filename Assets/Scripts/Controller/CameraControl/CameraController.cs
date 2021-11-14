using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
    , IPointerClickHandler
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

        // ī�޶�� �÷��̾������ �Ÿ� 
        SetCameraDistance(maxDistance);
    }

    public bool CursorLockState()
    {
        return Cursor.lockState == CursorLockMode.Locked;
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

    void LateUpdate()
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

    public void OnPointerClick(PointerEventData eventData)
    {
    }
}
