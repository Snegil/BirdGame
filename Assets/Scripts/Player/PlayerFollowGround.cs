using UnityEngine;

public class PlayerFollowGround : MonoBehaviour
{
    [SerializeField]
    float originYOffset = 0.5f;
    [SerializeField]
    float GroundCheckDistance = 0.25f;

    Rigidbody rb;

    [SerializeField]
    LayerMask layerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 origin = transform.position;
        origin.y += originYOffset;
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, GroundCheckDistance + originYOffset, layerMask))
        {
            Debug.Log(hit.collider.name);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            transform.position = new Vector3(transform.position.x, hit.point.y + GroundCheckDistance, transform.position.z);
        }
    }
}
