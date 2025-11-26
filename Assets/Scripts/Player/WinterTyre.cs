using UnityEngine;

public class WinterTyre : MonoBehaviour
{
    [SerializeField]
    bool rollerbladeOn = false;
    public bool RollerBladeOn
    {
        get { return rollerbladeOn; }
        set { rollerbladeOn = value; }
    }

    // My own linear damping due to regular rigidbody linear damping dampens Y axis, slowing the fall.
    public void CustomDamping(Rigidbody rb)
    {
        if (rollerbladeOn) return;

        Vector3 vel = rb.linearVelocity;

        // Reduce sliding only on the ground plane
        Vector3 horizontal = new(vel.x, 0f, vel.z);

        horizontal *= 0.85f; // friction factor

        rb.linearVelocity = new Vector3(horizontal.x, vel.y, horizontal.z);
    }

}
