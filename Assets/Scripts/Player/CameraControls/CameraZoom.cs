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

    [SerializeField]
    Transform cameraParent;

    void Start()
    {
        cameraTrigger = GetComponent<CameraTrigger>();
    }

    void LateUpdate()
    {
        RaycastHit hit = cameraRay.CameraPositionRay();
        Vector3 point = hit.collider != null ? hit.point : maxZoom;
        Vector3 localPosPoint = hit.collider != null ? transform.parent.InverseTransformPoint(hit.point) : maxZoom;

        Debug.Log(localPosPoint);

        float distanceToPoint = Vector3.Distance(cameraParent.position, point);
        float distanceToCamera = Vector3.Distance(cameraParent.position, transform.position);

        Debug.Log(distanceToPoint + " | " + distanceToCamera);

        if (distanceToPoint > distanceToCamera)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, localPosPoint, Time.deltaTime * cameraZoomSpeed);
            return;
        }

        transform.localPosition = localPosPoint;

        //TODO: FIX LERP. CURRENTLY DOES NOT FUNCTION AS INTENDED; INTENDED WORKCASE IS WHEN ZOOMING IN, IT'S IMMEDIATE AND ZOOMING OUT IS LERP.
        //transform.localPosition = Vector3.Lerp(transform.localPosition, point, Time.deltaTime * cameraZoomSpeed);
    }
}
