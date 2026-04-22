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

        float distanceToPoint = Vector3.Distance(cameraParent.position, point);
        float distanceToCamera = Vector3.Distance(cameraParent.position, transform.position);

        if (distanceToPoint > distanceToCamera)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, localPosPoint, Time.deltaTime * cameraZoomSpeed);
            return;
        }

        transform.localPosition = localPosPoint;
    }
}
