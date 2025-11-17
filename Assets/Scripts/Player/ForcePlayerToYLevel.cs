using UnityEngine;

public class ForcePlayerToYLevel : MonoBehaviour
{   
    [SerializeField]
    float GroundCheckDistance = 0.25f;

    GroundCheck groundCheck;
    Rigidbody rb;
    RigidbodyConstraints constraints;

    [SerializeField]
    LayerMask layerMask;

    void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody>();
        constraints = rb.constraints;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.down * GroundCheckDistance, Color.green);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, GroundCheckDistance, layerMask))
        {
            rb.linearVelocity = new Vector3(0, 0, 0);
            rb.position = new Vector3(transform.position.x, hit.point.y + GroundCheckDistance, transform.position.z);
        }
    }
}
