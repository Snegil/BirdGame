using UnityEngine;

public class ForcePlayerToYLevel : MonoBehaviour
{
    [SerializeField]
    float GroundCheckDistance = 0.25f;

    [SerializeField]
    LayerMask layerMask;

    // Update is called once per frame
    public void ToYLevel(Rigidbody rb)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, GroundCheckDistance, layerMask))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            transform.position = new Vector3(transform.position.x, hit.point.y + GroundCheckDistance, transform.position.z);
        }
    }
}
