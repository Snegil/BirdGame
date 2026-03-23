using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateRun", menuName = "PlayerStates/PlayerStateRun", order = 4)]
public class PlayerStateRun : ScriptableObject
{
    [SerializeField]
    float sprintSpeed = 1.5f;

    [SerializeField]
    float airbourneSpeed = 0.5f;

    [SerializeField]
    float maxSpeed = 10;

    CustomLinearDamping linearDamping = new();

    public void Run(Transform waypoint, GameObject gameObject, GroundCheck groundCheck, GameObject playerModel, Rigidbody rb)
    {
        Vector3 movementDirection = waypoint.position - gameObject.transform.position;
        movementDirection.Normalize(); // Normalize the direction vector

        playerModel.transform.LookAt(waypoint);

        if (groundCheck.GroundedCheck(0.1f))
        {
            // * 10 is only for making the movementSpeed and sprintSpeed numbers not so humongous
            rb.AddForce(10 * Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) * sprintSpeed * movementDirection);
        }
        else
        {
            // * 10 is only for making the movementSpeed and sprintSpeed numbers not so humongous
            rb.AddForce(10 * airbourneSpeed * Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) * sprintSpeed * movementDirection);
        }
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        linearDamping.CustomDamping(rb);
    }
}
