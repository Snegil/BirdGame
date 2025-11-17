using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GroundCheck))]
public class PlayerMovement : MonoBehaviour
{
    public delegate void IsMovingEvent(bool value, Vector3 speed);
    public event IsMovingEvent IsMoving;

    [SerializeField]
    bool isMoving = false;
    [SerializeField]
    bool isJumping = false;

    GroundCheck groundCheck;
    [SerializeField]
    float groundCheckDistance;
    bool grounded = false;

    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float jumpPower = 2;
    
    Vector3 movementValue;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float gravity = 9.8f;

    void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //grounded = groundCheck.GroundedCheck(groundCheckDistance);

        if (!groundCheck.GroundedCheck(groundCheckDistance) && !isJumping)
        {
            HandleGravity();            
        }
        if(isMoving)
        {
            transform.Translate(movementSpeed * Time.deltaTime * movementValue);
        }
        if (isJumping)
        {
            Jump();
        }
    }

    void HandleGravity()
    {
        transform.Translate(0, -gravity * Time.deltaTime, 0);
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector3>();

        if (context.started)
        {
            isMoving = true;
            IsMoving?.Invoke(isMoving, movementValue);
        }
        if (context.canceled)
        {
            isMoving = false;
            IsMoving?.Invoke(isMoving, movementValue);
        }
    }
    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isJumping = true;
        }
        if (context.canceled)
        {
            isJumping = false;
        }
    }

    void Jump()
    {
        //transform.Translate(0, jumpPower * Time.deltaTime, 0);
        transform.position = new Vector3(transform.position.x, jumpPower, transform.position.z);
    }
}
