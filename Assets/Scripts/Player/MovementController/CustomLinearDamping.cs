using UnityEngine;

public class CustomLinearDamping
{
    [Space, SerializeField]
    float frictionFactor = 0.85f;

    /// <summary>
    /// Linear Damping that does not affect the Y-axis.
    /// </summary>
    /// <param name="rb">Rigidbody</param>
    public void CustomDamping(Rigidbody rb)
    {
        Vector3 vel = rb.linearVelocity;

        // Reduce sliding only on the ground plane
        Vector3 horizontal = new(vel.x, 0f, vel.z);

        horizontal *= frictionFactor; // friction factor

        rb.linearVelocity = new Vector3(horizontal.x, vel.y, horizontal.z);
    }

    /// <summary>
    /// Linear Damping that does not affect the Y-axis.
    /// </summary>
    /// <param name="rb">Rigidbody</param>
    /// <param name="frictionFactor">Friction Factor</param>
    public void CustomDamping(Rigidbody rb, float frictionFactor = 20000)
    {
        Vector3 vel = rb.linearVelocity;

        // Reduce sliding only on the ground plane
        Vector3 horizontal = new(vel.x, 0f, vel.z);

        horizontal *= frictionFactor;

        rb.linearVelocity = new Vector3(horizontal.x, vel.y, horizontal.z);
    }
}
