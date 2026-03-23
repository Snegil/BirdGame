using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateWalk", menuName = "PlayerStates/PlayerStateWalk", order = 3)]
public class PlayerStateWalk : ScriptableObject
{
    [SerializeField]
    float walkSpeed = 1.5f;

    [SerializeField]
    float airbourneSpeed = 0.5f;

    [SerializeField]
    float maxSpeed = 10;

    CustomLinearDamping linearDamping = new();

    public void Walk(Transform waypoint, GameObject gameObject, GroundCheck groundCheck, GameObject playerModel, Rigidbody rb)
    {
        Vector3 movementDirection = waypoint.position - gameObject.transform.position;
        movementDirection.Normalize(); // Normalize the direction vector

        playerModel.transform.LookAt(waypoint);

        if (groundCheck.GroundedCheck(0.1f))
        {
            // * 10 is only for making the movementSpeed and sprintSpeed numbers not so humongous
            rb.AddForce(10 * Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) * walkSpeed * movementDirection);
        }
        else
        {
            // * 10 is only for making the movementSpeed and sprintSpeed numbers not so humongous
            rb.AddForce(10 * airbourneSpeed * Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) * walkSpeed * movementDirection);
        }
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        linearDamping.CustomDamping(rb);
    }
}
