using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [SerializeField]
    Transform shadow;
    [SerializeField]
    LayerMask layerMask;

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            shadow.SetPositionAndRotation(new Vector3(transform.position.x, hit.point.y + 0.02f, transform.position.z), Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation);
        }
        else
        {
            shadow.position = new Vector3(0, 0, 0);
        }
    }
}
