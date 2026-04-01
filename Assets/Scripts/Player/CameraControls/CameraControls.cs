using System;
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
    public bool CameraMoving { get { return cameraMoving; } }
    [Space, SerializeField]
    bool smartCamera = true;
    [SerializeField]
    float autoFollowBackwardsTolerance = 0.1f;

    Transform playerTransform;
    Rigidbody playerRB;

    [Space, SerializeField]
    Transform playerModel;

    [SerializeField]
    PlayerMovementController playerMovementController;
    public PlayerMovementController GetPlayerMovementController { get { return playerMovementController; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensitivity = mouseSensitivity;
        setTimeUntilFollow = timeUntilFollow;

        playerRB = playerModel.parent.GetComponent<Rigidbody>();
        playerTransform = playerModel.parent.transform;
    }

    void FixedUpdate()
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

        // Debug drawrays to visualise the different forward directions
        Debug.DrawRay(playerModel.position, playerModel.forward, Color.yellow);
        Debug.DrawRay(playerTransform.position, playerTransform.forward, Color.cyan);
        Debug.DrawRay(transform.position, transform.forward, Color.magenta);

        if (playerMovementController.RotatePlayerWithCamera && cameraMoving)
        {
            playerRB.rotation = Quaternion.Euler(playerRB.rotation.x, totalRotX, playerTransform.rotation.z);
        }

        if (!smartCamera) return;

        Vector3 camForward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 playerForward = new Vector3(playerModel.forward.x, 0, playerModel.forward.z).normalized;

        float forwardDot = Vector3.Dot(camForward, playerForward);

        //Debug.Log(movingForwardRelative + " " + Math.Round(forwardDot, 2) + " | " + autoFollowBackwardsTolerance);

        // This means "player is running forward relative to camera view"
        // Stops the camera and player to have different forward directions causing the controls to be inverted.
        bool movingForwardRelative = forwardDot > autoFollowBackwardsTolerance;

        if (playerMovementController.RotatePlayerWithCamera && !cameraMoving && timeUntilFollow <= 0 && movingForwardRelative)
        {
            autoRotX = Mathf.DeltaAngle(0, playerModel.rotation.eulerAngles.y);
            Quaternion autoTargetRot = Quaternion.Euler(autoRotYTarget, autoRotX, 0f);

            // Rotate the player towards the autotarget.
            playerRB.rotation = Quaternion.Lerp(playerRB.rotation, autoTargetRot, Time.deltaTime * lerpSpeed);
            // rotate Camera Parent towards the player rotation.
            // TODO: May not need lerp.
            transform.rotation = Quaternion.Lerp(transform.rotation, playerModel.rotation, Time.deltaTime * lerpSpeed);

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
            cameraMoving = false;
        }

        sensitivity = context.control.device is Gamepad ? controllerSensitivity : mouseSensitivity;
        Cursor.visible = context.control.device is not Gamepad;
        Cursor.lockState = context.control.device is Gamepad ? CursorLockMode.None : CursorLockMode.Locked;

        mouseDelta = context.ReadValue<Vector2>();
    }
}
