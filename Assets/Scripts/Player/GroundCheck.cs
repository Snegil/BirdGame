using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField, Header("Length of raycast downwards.")]
    float distance = 1f;
    [SerializeField]
    Vector3 boxCastSize = new(0.1f, 0.1f, 0.1f);

    [SerializeField, Header("Which layer to hit.")]
    LayerMask layerMask;

    public bool GroundedCheck(float length = -20f)
    {
        if (length == -20f) length = distance;

        //return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, length, layerMask);
        return Physics.BoxCast(transform.position + transform.up * 0.1f, boxCastSize, -transform.up, out RaycastHit hit, Quaternion.identity, length, layerMask);
    }
    public float DistanceFromGround(float length = -20f)
    {
        if (length == -20f) length = distance;
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, length, layerMask);
        return hit.distance;
    }
}
