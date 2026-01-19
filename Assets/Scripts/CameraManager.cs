using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera cam;

    [Header("Zoom Settings")]
    [SerializeField] float zoomSpeed = 0.2f;       // Zoom speed (8f for mouse)
    [SerializeField] float minZoom = 20f;           // Limit zoom
    [SerializeField] float maxZoom = 80f;          // Limit zoom 

    [Header("Pan Settings")]
    [SerializeField] float panSpeed = 0.2f;        // Camera pan speed (0.005 for mouse)

    private Vector3 lastPanPosition;     // Previous touch position
    private int panFingerId;             // ID finger pan

    private Vector3 lastMousePosition; // Test mouse zoom and pan

    void Start()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastPanPosition = touch.position;
                panFingerId = touch.fingerId;
            }
            else if (touch.phase == TouchPhase.Moved && touch.fingerId == panFingerId)
            {
                PanCamera(touch);
            }
        }
        else if (Input.touchCount == 2)
        {
            ZoomCamera();
        }

        //Test pan, zoom with mouse move, mouse scroll
        if (Input.GetMouseButtonDown(1)) // Giữ chuột giữa
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            Vector3 move = new Vector3(-delta.x * panSpeed / 10f, 0, -delta.y * panSpeed / 10f);
            cam.transform.Translate(move, Space.World);

            lastMousePosition = Input.mousePosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float fov = cam.fieldOfView;
            fov -= scroll * zoomSpeed * 40f;
            cam.fieldOfView = Mathf.Clamp(fov, minZoom, maxZoom);
        }
    }

    void PanCamera(Touch touch)
    {
        Vector3 currentPanPosition = touch.position;
        Vector3 delta = currentPanPosition - lastPanPosition;

        // Convert screen delta to world space movement
        Vector3 moveDirection = transform.right * -delta.x * panSpeed * Time.deltaTime;
        moveDirection += transform.up * -delta.y * panSpeed * Time.deltaTime;

        transform.Translate(moveDirection, Space.World);

        lastPanPosition = currentPanPosition;
    }

    void ZoomCamera()
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

        float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
        float currentMagnitude = (touch0.position - touch1.position).magnitude;

        float difference = currentMagnitude - prevMagnitude;

        cam.fieldOfView -= difference * zoomSpeed;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
    }
}
