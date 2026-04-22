using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GroundCheck), typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    public delegate void PlayerStateUpdate(PlayerStates playerState, bool rollerblades);
    public event PlayerStateUpdate PlayerStateChange;

    [Space, SerializeField]
    PlayerStates playerState;
    public PlayerStates GetPlayerStates { get { return playerState; } }

    // Set to true if the player should rotate when moving the camera.
    public bool RotatePlayerWithCamera { get; set; } = false;

    [Space, SerializeField, Header("Deadzone distance from centre of player character:")]
    float deadZone;

    [Space, SerializeField]
    Transform waypoint;

    bool isJumping = false;
    public bool IsJumping { get { return isJumping; } }

    [Space, Space]

    [SerializeField]
    PlayerStateIdle idle;
    [SerializeField]
    PlayerStateMovement walk;
    [SerializeField]
    PlayerStateMovement run;
    [SerializeField]
    PlayerStateMovement rollerblade;
    [SerializeField]
    PlayerStateJump jump;

    [Space, Space]

    bool isRunning = false;

    GroundCheck groundCheck;
    Rigidbody rb;

    [SerializeField]
    GameObject playerModel;

    [SerializeField]
    float rollerbladeCD = 10f;
    float setRollerbladeCD;
    bool rollerbladeToggle = false;
    [SerializeField]
    GameObject uggs;
    [SerializeField]
    GameObject rollerblades;

    [SerializeField]
    float fasterDownwardforce = 0.2f;

    bool jumpButtonPressed = false;

    [Space, SerializeField]
    Animator animator;

    bool hitGround;

    PlayerStates stateWhenJumpInput;

    float distanceFromWaypoint;

    void Start()
    {
        setRollerbladeCD = rollerbladeCD;
        rollerbladeCD = 0;

        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rollerbladeCD > 0) rollerbladeCD -= Time.deltaTime;

        Vector3 movementDirection = waypoint.position - gameObject.transform.position;
        movementDirection.y = 0; // Set the y value to 0 to prevent vertical movement
        movementDirection.Normalize(); // Normalize the direction vector

        distanceFromWaypoint = Vector3.Distance(waypoint.position, transform.position);

        switch (playerState)
        {
            case PlayerStates.Idle:
                Idle();
                break;

            case PlayerStates.Walk:
                Walk();
                break;

            case PlayerStates.Run:
                Run();
                break;

            case PlayerStates.Rollerblades:
                Rollerblade();
                break;

            case PlayerStates.Jump:
                Jump();
                break;

            case PlayerStates.Land:
                Land();
                break;
        }
    }

    void Idle()
    {
        // IF JUMP BUTTON HAS BEEN PRESSED, CHANGE STATE TO JUMP.
        if (jumpButtonPressed)
        {
            //Debug.Log("JUMP BUTTON PRESSED ENTERED IN IDLE CASE");
            ChangeState(PlayerStates.Jump);
        }

        // IF DISTANCE FROM THE WAYPOINT MOVED BY INPUTMANAGER.CS IS GREATER THAN OR EQUAL TO DEADZONE VARIABLE, CHANGE STATE TO WALK.
        if (distanceFromWaypoint > deadZone)
        {
            ChangeState(PlayerStates.Walk);
            return;
        }
        if (groundCheck.GroundedCheck(0.25f) && !rollerbladeToggle) rb.linearVelocity = Vector3.zero;
        RotatePlayerWithCamera = false;
        idle.Idle(groundCheck);
    }

    void Walk()
    {
        // IF JUMP BUTTON HAS BEEN PRESSED, CHANGE STATE TO JUMP.
        if (jumpButtonPressed)
        {
            //Debug.Log("JUMP BUTTON PRESSED ENTERED IN WALK CASE");
            ChangeState(PlayerStates.Jump);
            return;
        }
        // IF ISRUNNING == TRUE, CHANGE STATE TO RUN.
        if (isRunning)
        {
            //Debug.Log("IS RUNNING");
            ChangeState(PlayerStates.Run);
            return;
        }
        // IF DISTANCE FROM THE WAYPOINT MOVED BY INPUTMANAGER.CS IS LESS THAN DEADZON VARIABLE, CHANGE STATE TO IDLE.
        if (distanceFromWaypoint < deadZone)
        {
            ChangeState(PlayerStates.Idle);
            return;
        }
        if (rollerbladeToggle)
        {
            ChangeState(PlayerStates.Rollerblades);
            return;
        }
        RotatePlayerWithCamera = true;
        walk.Move(waypoint, gameObject, groundCheck, playerModel, rb);
        animator.SetFloat("SpeedMultiplier", distanceFromWaypoint);
    }

    void Run()
    {
        // IF JUMP BUTTON HAS BEEN PRESSED, CHANGE STATE TO JUMP.
        if (jumpButtonPressed)
        {
            ChangeState(PlayerStates.Jump);
            jumpButtonPressed = false;
            return;
        }
        // IF ISRUNNING == FALSE, CHANGE STATE TO WALK.
        if (!isRunning)
        {
            ChangeState(PlayerStates.Walk);
            return;
        }
        // IF DISTANCE FROM THE WAYPOINT MOVED BY INPUTMANAGER.CS IS LESS THAN DEADZON VARIABLE, CHANGE STATE TO IDLE.
        if (distanceFromWaypoint <= deadZone)
        {
            ChangeState(PlayerStates.Idle);
            return;
        }
        if (rollerbladeToggle)
        {
            ChangeState(PlayerStates.Rollerblades);
            return;
        }
        RotatePlayerWithCamera = true;
        run.Move(waypoint, gameObject, groundCheck, playerModel, rb);
    }

    void Rollerblade()
    {
        // IF JUMP BUTTON HAS BEEN PRESSED, CHANGE STATE TO JUMP.
        if (jumpButtonPressed)
        {
            ChangeState(PlayerStates.Jump);
            jumpButtonPressed = false;
            return;
        }
        // IF DISTANCE FROM THE WAYPOINT MOVED BY INPUTMANAGER.CS IS LESS THAN DEADZON VARIABLE, CHANGE STATE TO IDLE.
        if (distanceFromWaypoint <= deadZone)
        {
            ChangeState(PlayerStates.Idle);
            return;
        }
        if (!rollerbladeToggle)
        {
            ChangeState(PlayerStates.Run);
            return;
        }
        RotatePlayerWithCamera = true;
        rollerblade.Move(waypoint, gameObject, groundCheck, playerModel, rb);
    }

    void Jump()
    {
        if (hitGround && isJumping)
        {
            ChangeState(PlayerStates.Land);
            return;
        }

        if (stateWhenJumpInput == PlayerStates.Rollerblades)
        {
            rollerblade.Move(waypoint, gameObject, groundCheck, playerModel, rb);

        }
        else if (stateWhenJumpInput == PlayerStates.Run)
        {
            run.Move(waypoint, gameObject, groundCheck, playerModel, rb);
        }
        else
        {
            walk.Move(waypoint, gameObject, groundCheck, playerModel, rb);
        }
        RotatePlayerWithCamera = true;

        // FASTER DOWNFORCE
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * (1 + fasterDownwardforce), rb.linearVelocity.z);
        }

        // IF NOT JUMPING, JUMP
        if (!isJumping)
        {
            isJumping = true;
            jump.Jump(rb);
            jumpButtonPressed = false;
            hitGround = false;
        }
    }

    void Land()
    {
        ChangeState(PlayerStates.Idle);
        isJumping = false;
        walk.Move(waypoint, gameObject, groundCheck, playerModel, rb);
        RotatePlayerWithCamera = true;
    }


    void ChangeState(PlayerStates state)
    {
        playerState = state;
        PlayerStateChange?.Invoke(state, rollerbladeToggle);
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (!context.started || isJumping == true) return;
        stateWhenJumpInput = playerState == PlayerStates.Jump ? PlayerStates.Walk : playerState;
        jumpButtonPressed = true;
    }

    public void SprintInput(InputAction.CallbackContext context)
    {
        if (context.started) isRunning = true;
        if (context.canceled) isRunning = false;
    }

    public void ToggleRollerblades(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (rollerbladeCD > 0) return;

        rollerbladeToggle = !rollerbladeToggle;
        rollerbladeCD = setRollerbladeCD;

        uggs.SetActive(!rollerbladeToggle);
        rollerblades.SetActive(rollerbladeToggle);
    }

    public void HitGround(bool value)
    {
        hitGround = value;
    }
    public bool PlayerMoving()
    {
        if (playerState == PlayerStates.Idle) return false;

        return true;
    }
}
