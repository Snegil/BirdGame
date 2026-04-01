using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public bool HitObject { get; set; } = false;

    CameraControls cameraControls;
    PlayerMovementController playerMovementController;


    void Start()
    {
        cameraControls = transform.parent.GetComponent<CameraControls>();
        playerMovementController = cameraControls.GetPlayerMovementController;
    }

    void OnTriggerEnter(Collider other)
    {
        if (cameraControls.CameraMoving || playerMovementController.PlayerMoving()) HitObject = true;
    }
    void OnTriggerExit(Collider other)
    {
        HitObject = false;
    }
}
