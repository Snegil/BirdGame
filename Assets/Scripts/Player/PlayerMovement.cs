using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public delegate void IsMovingEvent(bool value);
    public event IsMovingEvent IsMoving;

    Rigidbody rb;

    bool canMove = true;
    bool isMoving = false;

    [SerializeField]
    float movementSpeed;

    Vector3 movementValue;

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
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector3>();

        if (canMove == false) return;

        if (context.started)
        {
            isMoving = true;
            IsMoving?.Invoke(isMoving);
        }
        if (context.canceled)
        {
            isMoving = false;
            IsMoving?.Invoke(isMoving);
            rb.linearVelocity = Vector3.zero;
        }

    }
}
