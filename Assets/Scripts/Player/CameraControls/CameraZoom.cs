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

    float lerpTime = 0;

    [SerializeField]
    float rayDistance = 0.1f;

    [SerializeField]
    float distanceTolerance = 0.1f;

    [SerializeField]
    LayerMask layerMask;

    void Start()
    {
        cameraTrigger = GetComponent<CameraTrigger>();
    }

    void LateUpdate()
    {
        // Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, rayDistance, layerMask);
        // Debug.DrawRay(transform.position, -transform.forward * distanceTolerance, Color.yellow);

        if (!cameraTrigger.HitObject) { lerpTime -= Time.deltaTime * cameraZoomSpeed; }
        else if (cameraTrigger.HitObject) { lerpTime += Time.deltaTime * cameraZoomSpeed; }

        lerpTime = Mathf.Clamp(lerpTime, 0, 1);
        transform.localPosition = Vector3.Lerp(maxZoom, minZoom, lerpTime);
    }
}
