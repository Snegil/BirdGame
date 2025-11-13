using UnityEngine;

public class RotationController : MonoBehaviour
{
    PlayerMovement playerMovement;
    Transform playerTransform;

    CameraControls cameraControls;
    Transform cameraTransform;

    bool playerIsMoving = false;
    bool cameraIsMoving = false;    

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerTransform = playerMovement.transform;

        cameraControls = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraControls>();
        cameraTransform = cameraControls.transform;
    }

    void OnEnable()
    {
        playerMovement.IsMoving += PlayerIsMoving;
        cameraControls.IsMoving += CameraIsMoving;
    }
    void OnDisable()
    {
        playerMovement.IsMoving -= PlayerIsMoving;
        cameraControls.IsMoving -= CameraIsMoving;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsMoving && cameraIsMoving) 
        {
            playerTransform.forward = cameraTransform.forward;
            return;
        }
    }

    void PlayerIsMoving(bool value)
    {
        playerIsMoving = value;
    }
    void CameraIsMoving(bool value)
    {
        cameraIsMoving = value;
    }
}
