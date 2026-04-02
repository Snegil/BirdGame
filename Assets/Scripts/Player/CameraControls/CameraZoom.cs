using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    float cameraZoomSpeed;
    [SerializeField]
    Vector3 minZoom;
    [SerializeField]
    Vector3 maxZoom;

    CameraTrigger cameraTrigger;

    float lerpTime = 1;

    [SerializeField]
    float rayForwardDistance = 0.4f;
    [SerializeField]
    float rayBackwardDistance = 0.1f;

    [SerializeField]
    float distanceTolerance = 0.1f;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    CameraRay cameraRay;

    void Start()
    {
        cameraTrigger = GetComponent<CameraTrigger>();
    }

    void LateUpdate()
    {
        RaycastHit hit = cameraRay.CameraPositionRay();
        Vector3 point = hit.collider != null ? transform.parent.InverseTransformPoint(hit.point) : maxZoom;

        Debug.Log(point);

        transform.localPosition = point;

        //TODO: FIX LERP. CURRENTLY DOES NOT FUNCTION AS INTENDED; INTENDED WORKCASE IS WHEN ZOOMING IN, IT'S IMMEDIATE AND ZOOMING OUT IS LERP.
        //transform.localPosition = Vector3.Lerp(transform.localPosition, point, Time.deltaTime * cameraZoomSpeed);
    }
}
