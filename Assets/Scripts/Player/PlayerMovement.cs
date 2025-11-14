using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public delegate void IsMovingEvent(bool value, float speed);
    public event IsMovingEvent IsMoving;

    Rigidbody rb;

    bool canMove = true;
    bool isMoving = false;

    [SerializeField]
    float movementSpeed;

    Vector3 movementValue;

    [SerializeField]
    float maxSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();       
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            rb.AddRelativeForce(movementSpeed * movementValue);
            rb.linearVelocity = new(Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed), rb.linearVelocity.y, Mathf.Clamp(rb.linearVelocity.z, -maxSpeed, maxSpeed));
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector3>();

        if (canMove == false) return;

        if (context.started)
        {
            isMoving = true;
            IsMoving?.Invoke(isMoving, rb.linearVelocity.x);
        }
        if (context.canceled)
        {
            isMoving = false;
            IsMoving?.Invoke(isMoving, rb.linearVelocity.x);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }
}
