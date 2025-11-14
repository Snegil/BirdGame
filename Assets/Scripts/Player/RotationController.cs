using UnityEngine;

public class RotationController : MonoBehaviour
{
    PlayerMovement playerMovement;
    Transform playerTransform;

    CameraControls cameraParentControls;
    Transform cameraParentTransform;

    bool playerIsMoving = false;
    bool cameraIsMoving = false;    

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerTransform = playerMovement.transform;

        cameraParentControls = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraControls>();
        cameraParentTransform = cameraParentControls.transform;
    }

    void OnEnable()
    {
        playerMovement.IsMoving += PlayerIsMoving;
        cameraParentControls.IsMoving += CameraIsMoving;
    }
    void OnDisable()
    {
        playerMovement.IsMoving -= PlayerIsMoving;
        cameraParentControls.IsMoving -= CameraIsMoving;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(
            0f,
            transform.eulerAngles.y,
            0f
        );
        float yRotation = cameraParentTransform.transform.rotation.eulerAngles.y;

        if (playerIsMoving && !cameraIsMoving)
        {
            playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);
            //playerTransform.forward = cameraTransform.forward;
        }
        if (playerIsMoving && cameraIsMoving) 
        {
            playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);
            //playerTransform.forward = cameraTransform.forward;            
            return;
        }
    }

    void PlayerIsMoving(bool value, float speed)
    {
        playerIsMoving = value;
    }
    void CameraIsMoving(bool value)
    {
        cameraIsMoving = value;
    }
}
