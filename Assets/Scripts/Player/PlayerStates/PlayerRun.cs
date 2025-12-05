using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 1.5f;

    [SerializeField, Header("Sprint Speed is multiplied with the movementSpeed, so 2 = double from movementSpeed")]
    float sprintSpeed = 2f;
    float sprint = 1;

    [SerializeField]
    float airbourneSpeed = 0.5f;

    bool isRunning = false;

    [SerializeField]
    float maxSpeed = 10;

    public void Run(PlayerStates playerState, Transform waypoint, Animator animator, GroundCheck groundCheck, GameObject playerModel, Rigidbody rb, bool isRollerbladeOn, bool runAnim)
    {
        if (runAnim && !animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Movement");
        }
        if (runAnim)
        {
            animator.speed = isRunning && !isRollerbladeOn ? 2 : Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1);
        }

        Vector3 movementDirection = waypoint.position - gameObject.transform.position;
        movementDirection.Normalize(); // Normalize the direction vector

        playerModel.transform.LookAt(waypoint);

        if (groundCheck.GroundedCheck())
        {
            // 10 is only for making the movementSpeed and sprintSpeed numbers not so humongous
            rb.AddForce(Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) * movementSpeed * 10 * sprint * movementDirection);
        }
        else
        {
            // 10 is only for making the movementSpeed and sprintSpeed numbers not so humongous
            rb.AddForce(10 * airbourneSpeed * Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) * movementSpeed * sprint * movementDirection);
        }
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRunning = true;
            sprint = sprintSpeed;
        }
        if (context.canceled)
        {
            isRunning = false;
            sprint = 1;
        }
    }
    public void StopAngularVelocity(Rigidbody rb)
    {
        rb.angularVelocity = Vector3.zero;
    }
}
