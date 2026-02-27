using UnityEngine;

public class WinterTyre : MonoBehaviour
{
    [SerializeField]
    bool dampingEnabled = false;
    [SerializeField]
    float frictionFactor = 0.85f;

    public bool DampingEnabled
    {
        get { return dampingEnabled; }
        set { dampingEnabled = value; }
    }

    // My own linear damping due to regular rigidbody linear damping dampens Y axis, slowing the fall.
    public void CustomDamping(Rigidbody rb)
    {
        if (dampingEnabled) return;
        Vector3 vel = rb.linearVelocity;

        // Reduce sliding only on the ground plane
        Vector3 horizontal = new(vel.x, 0f, vel.z);

        horizontal *= frictionFactor; // friction factor

        rb.linearVelocity = new Vector3(horizontal.x, vel.y, horizontal.z);
    }

}
