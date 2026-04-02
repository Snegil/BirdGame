using UnityEngine;

public class CameraRay : MonoBehaviour
{
    [SerializeField]
    float rayBackwardDistance = 2f;

    [SerializeField]
    LayerMask layerMask;

    public RaycastHit CameraPositionRay()
    {
        Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, rayBackwardDistance, layerMask);
        float distance = hit.collider != null ? hit.distance : rayBackwardDistance;
        Debug.DrawRay(transform.position, -transform.forward * distance, Color.yellow);
        return hit;
    }
}
