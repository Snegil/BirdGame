using UnityEngine;

public class RotationController : MonoBehaviour
{
    PlayerMovement playerMovement;
    Transform playerTransform;

    CameraControls cameraParentControls;
    Transform cameraParentTransform;

    bool playerIsMoving = false;
    bool cameraIsMoving = false;    

    Vector3 playerDirection;

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
        float yRotation = cameraParentTransform.transform.rotation.eulerAngles.y;

        if (playerIsMoving && !cameraIsMoving)
        {
            playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        if (playerIsMoving && cameraIsMoving) 
        {
            playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);   
            return;
        }
    }

    void PlayerIsMoving(bool value, Vector3 speed)
    {
        playerIsMoving = value;
        playerDirection = speed;
    }
    void CameraIsMoving(bool value)
    {
        cameraIsMoving = value;
    }
}
