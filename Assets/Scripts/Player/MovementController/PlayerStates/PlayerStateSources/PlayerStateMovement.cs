using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateMovement", menuName = "PlayerStates/PlayerStateMovement", order = 3)]
public class PlayerStateMovement : ScriptableObject
{
    [SerializeField]
    float speed = 1.5f;

    [SerializeField]
    float airbourneSpeed = 0.5f;

    [SerializeField]
    float maxSpeed = 10;

    [Space, SerializeField]
    bool customFriction = false;
    [SerializeField]
    float frictionAmount = 0.85f;

    CustomLinearDamping linearDamping = new();

    public void Move(Transform waypoint, GameObject gameObject, GroundCheck groundCheck, GameObject playerModel, Rigidbody rb)
    {
        Vector3 movementDirection = waypoint.position - gameObject.transform.position;
        movementDirection.Normalize(); // Normalize the direction vector

        playerModel.transform.LookAt(waypoint);

        if (groundCheck.GroundedCheck(0.1f))
        {
            // * 10 is only for making the movementSpeed and sprintSpeed numbers not so humongous
            rb.AddForce(10 * Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) * speed * movementDirection);
        }
        else
        {
            // * 10 is only for making the movementSpeed and sprintSpeed numbers not so humongous
            rb.AddForce(10 * airbourneSpeed * Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) * speed * movementDirection);
        }
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        if (customFriction == false)
        {
            linearDamping.CustomDamping(rb);
            return;
        }
        linearDamping.CustomDamping(rb, frictionAmount);
    }
}
