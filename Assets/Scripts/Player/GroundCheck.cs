using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField, Header("Length of raycast downwards.")]
    float distance = 1f;

    [SerializeField, Header("Which layer to hit.")]
    LayerMask layerMask;

    public bool GroundedCheck(float length = -20f)
    {
        if (length == -20f) length = distance;
        
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, length, layerMask);
    }
}
