using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Transform raycastOrigin;

    void FixedUpdate()
    {
        Physics.Raycast(raycastOrigin.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask);

        transform.position = hit.collider != null ? transform.position = new Vector3(hit.point.x, hit.point.y + 0.001f, hit.point.z) : new Vector3(0, -100, 0);
    }
}
