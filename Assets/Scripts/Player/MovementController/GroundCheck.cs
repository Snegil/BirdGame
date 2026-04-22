using Unity.Mathematics;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField, Header("Length of raycast downwards.")]
    float distance = 1f;
    [SerializeField]
    Vector3 boxCastSize = new(0.1f, 0.1f, 0.1f);

    [SerializeField, Header("Which layer to hit.")]
    LayerMask layerMask;

    void Update()
    {
        DrawboxCustom.DrawBoxCastBox(transform.position, boxCastSize, quaternion.identity, -transform.up, distance, Color.grey);
    }
    public bool GroundedCheck(float length = -20f)
    {
        if (length == -20f) length = distance;

        //return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, length, layerMask);
        return Physics.BoxCast(transform.position + new Vector3(0, 0.1f, 0), boxCastSize, -transform.up, out RaycastHit hit, Quaternion.identity, length, layerMask);
    }
    public float DistanceFromGround(float length = -20f)
    {
        if (length == -20f) length = distance;
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, length, layerMask);
        return hit.distance;
    }
    // void OnDrawGizmos()
    // {
    //     Vector3 centre = new(transform.position.x, transform.position.y - 0.1f, transform.position.z);
    //     Gizmos.DrawCube(transform.position - transform.up * distance, boxCastSize);
    // }
}
