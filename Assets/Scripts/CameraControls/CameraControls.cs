using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public delegate void IsMovingEvent(bool value);
    public event IsMovingEvent IsMoving;

    float sensitivity;

    [SerializeField]
    float mouseSensitivity = 1f;
    [SerializeField]
    float controllerSensitivity = 20f;

    [SerializeField]
    float rotationLimitX;
    [SerializeField]
    float rotationLimitY;

    Vector2 mouseDelta;

    float totalRotX = 0;
    float totalRotY = 0;

    [SerializeField]
    Rigidbody rb;

    bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensitivity = mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        totalRotX += mouseDelta.x * sensitivity * Time.deltaTime;
        totalRotY += mouseDelta.y * sensitivity * Time.deltaTime;

        //totalRotX = Mathf.Clamp(totalRotX, -rotationLimitX, rotationLimitX);
        totalRotY = Mathf.Clamp(totalRotY, -rotationLimitY, rotationLimitY);

        transform.rotation = Quaternion.Euler(-totalRotY, totalRotX, 0f);
        if (rb == null) return;
        rb.rotation = Quaternion.Euler(-totalRotY, totalRotX, 0f);
    }

    public void MouseDelta(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isMoving = true;
            IsMoving?.Invoke(isMoving);
        }
        if (context.canceled)
        {
            isMoving  = false;
            IsMoving?.Invoke(isMoving);
        }
        
        if (context.control.device is Gamepad)
        {
            Debug.Log("CONTROLLER");
            sensitivity = controllerSensitivity;
        }
        else
        {
            Debug.Log("MOUSE");
            sensitivity = mouseSensitivity;
        }



        mouseDelta = context.ReadValue<Vector2>();

        //totalRotX += mouseDelta.x * mouseSensitivity * Time.deltaTime;
        //totalRotY += mouseDelta.y * mouseSensitivity * Time.deltaTime;

        //totalRotX = Mathf.Clamp(totalRotX, -rotationLimitX, rotationLimitX);
        //totalRotY = Mathf.Clamp(totalRotY, -rotationLimitY, rotationLimitY);
    }
}
