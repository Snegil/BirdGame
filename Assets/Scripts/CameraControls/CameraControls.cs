using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    float sensitivity;

    [SerializeField]
    float mouseSensitivity = 1f;
    [SerializeField]
    float controllerSensitivity = 20f;

    [SerializeField]
    float timeUntilFollow = 5f;
    float setTimeUntilFollow;

    [SerializeField, Header("Set the max rotation for the Y axis\nX = MIN, Y = MAX")]
    Vector2 rotationLimitY;
    [SerializeField]
    float autoRotYTarget = -9;
    float autoRotX;

    [SerializeField]
    float lerpSpeed = 2;

    Vector2 mouseDelta;

    float totalRotX = 0;
    float totalRotY = 0;

    bool cameraMoving = false;
    [Space, SerializeField]
    bool smartCamera = true;
    [SerializeField]
    float autoFollowBackwardsTolerance = 0.1f;

    [Space, SerializeField]
    Transform player;

    [SerializeField]
    PlayerController playerController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensitivity = mouseSensitivity;
        setTimeUntilFollow = timeUntilFollow;
    }
    void LateUpdate()
    {
        totalRotX += mouseDelta.x * sensitivity * Time.deltaTime;
        totalRotY += mouseDelta.y * sensitivity * Time.deltaTime;
        totalRotY = Mathf.Clamp(totalRotY, rotationLimitY.x, rotationLimitY.y);

        if (timeUntilFollow > 0)
        {
            timeUntilFollow -= Time.deltaTime;    
        }        

        if (cameraMoving)
        {
            transform.rotation = Quaternion.Euler(-totalRotY, totalRotX, 0f);
            timeUntilFollow = setTimeUntilFollow;
        }
        Debug.DrawRay(player.position, player.forward, Color.yellow);
        Debug.DrawRay(player.parent.position, player.parent.forward, Color.cyan);
        Debug.DrawRay(transform.position, transform.forward, Color.magenta);
    }
    void FixedUpdate()
    {
        if (playerController.AllowCamControl && cameraMoving)
        {
            player.parent.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, totalRotX, 0f);
        }

        if (!smartCamera) return;

        Vector3 camForward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 playerForward = new Vector3(player.forward.x, 0, player.forward.z).normalized;

        float forwardDot = Vector3.Dot(camForward, playerForward);

        // This means "player is running forward relative to camera view"
        bool movingForwardRelative = forwardDot > autoFollowBackwardsTolerance;

        if (playerController.AllowCamControl && !cameraMoving && timeUntilFollow <= 0 && movingForwardRelative)
        {
            autoRotX = Mathf.DeltaAngle(0, player.rotation.eulerAngles.y);
            Quaternion autoTargetRot = Quaternion.Euler(autoRotYTarget, autoRotX, 0f);

            player.parent.GetComponent<Rigidbody>().rotation = Quaternion.Lerp(player.parent.GetComponent<Rigidbody>().rotation, autoTargetRot, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, Time.deltaTime * lerpSpeed);

            totalRotX = Mathf.DeltaAngle(0, transform.rotation.eulerAngles.y);
            totalRotY = Mathf.DeltaAngle(0, -transform.rotation.eulerAngles.x);
        }
    }

    public void MouseDelta(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            cameraMoving = true;
        }
        if (context.canceled)
        {
            cameraMoving  = false;
        }
        
        sensitivity = context.control.device is Gamepad ? controllerSensitivity : mouseSensitivity;
        Cursor.visible = context.control.device is not Gamepad;
        Cursor.lockState = context.control.device is Gamepad ? CursorLockMode.None : CursorLockMode.Locked;

        mouseDelta = context.ReadValue<Vector2>();
    }
}
