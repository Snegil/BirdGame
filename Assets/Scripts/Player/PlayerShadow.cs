using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Transform raycastOrigin;

    [SerializeField]
    float groundTolerance = 0.001f;

    void FixedUpdate()
    {
        Physics.Raycast(raycastOrigin.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask);
        if (hit.collider == null)
        {
            transform.position = new Vector3(0, -100, 0);
            return;
        }
        transform.position = new Vector3(hit.point.x, hit.point.y + groundTolerance, hit.point.z);
        transform.up = hit.normal;

    }
}
