using UnityEngine;

public class CustomLinearDamping : MonoBehaviour
{
    [field: SerializeField]
    public bool DampingEnabled { get; set; } = false;
    [Space, SerializeField]
    float frictionFactor = 0.85f;

    /// <summary>
    /// Linear Damping that does not affect the Y-axis.
    /// </summary>
    /// <param name="rb">Rigidbody</param>
    public void CustomDamping(Rigidbody rb, float? frictionFactor = null)
    {
        frictionFactor ??= this.frictionFactor;

        if (DampingEnabled) return;
        Vector3 vel = rb.linearVelocity;

        // Reduce sliding only on the ground plane
        Vector3 horizontal = new(vel.x, 0f, vel.z);

        horizontal *= frictionFactor == null ? 0 : 1; // friction factor

        rb.linearVelocity = new Vector3(horizontal.x, vel.y, horizontal.z);
    }
}
