using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    Vector3[] zoomLevels;

    int cameraZoomIndex = 0;

    void LateUpdate()
    {
        if (cameraZoomIndex >= zoomLevels.Length) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, zoomLevels[cameraZoomIndex], Time.deltaTime);
    }

    public void ChangeCameraZoom(InputAction.CallbackContext context)
    {
        if (!context.started) return;


        if (cameraZoomIndex < zoomLevels.Length)
        {
            cameraZoomIndex++;            
        }
        else
        {
            cameraZoomIndex = 0;
        }

        // if (cameraZoomIndex > zoomLevels.Length) cameraZoomIndex = 0;
    }
}
