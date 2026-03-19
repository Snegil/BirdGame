using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GroundCheck), typeof(Rigidbody), typeof(CustomLinearDamping))]
public class PlayerMovementController : MonoBehaviour
{
    public delegate void PlayerStateUpdate(PlayerStates playerState, bool rollerblades);
    public event PlayerStateUpdate PlayerStateChange;

    [Space, SerializeField]
    PlayerStates playerState;

    // Set to true if the player should rotate when moving the camera.
    public bool RotatePlayerWithCamera { get; set; } = false;

    [Space, SerializeField, Header("Deadzone distance from centre of player character:")]
    float deadZone;

    [Space, SerializeField]
    Transform waypoint;

    bool isJumping = false;

    [Space, Space]

    [SerializeField]
    PlayerStateIdle idle;
    [SerializeField]
    PlayerStateWalk walk;
    [SerializeField]
    PlayerStateRun run;
    [SerializeField]
    PlayerStateJump jump;

    [Space, Space]

    bool isRunning = false;

    CustomLinearDamping customDamping;

    GroundCheck groundCheck;
    Rigidbody rb;

    [SerializeField]
    GameObject playerModel;

    [SerializeField]
    float rollerbladeCD = 10f;
    float setRollerbladeCD;

    [SerializeField]
    List<GameObject> RegularBoots = new();
    [SerializeField]
    GameObject Rollerblades;

    [SerializeField]
    float fasterDownwardforce = 0.2f;

    bool jumpButtonPressed = false;

    [Space, SerializeField]
    Animator animator;

    bool hitGround;

    PlayerStates stateWhenJumpInput;

    [SerializeField]
    Transform attackOrigin;

    void Start()
    {
        setRollerbladeCD = rollerbladeCD;
        rollerbladeCD = 0;

        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody>();
        customDamping = GetComponent<CustomLinearDamping>();
    }

    void FixedUpdate()
    {
        if (rollerbladeCD > 0) rollerbladeCD -= Time.deltaTime;

        Vector3 movementDirection = waypoint.position - gameObject.transform.position;
        movementDirection.y = 0; // Set the y value to 0 to prevent vertical movement
        movementDirection.Normalize(); // Normalize the direction vector

        float distanceFromWaypoint = Vector3.Distance(waypoint.position, transform.position);

        //Debug.Log(distanceFromWaypoint + "| Unclamped: " + Vector3.Distance(waypoint.position, transform.position));

        switch (playerState)
        {
            case PlayerStates.Idle:
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
                RotatePlayerWithCamera = false;
                idle.Idle(groundCheck);

                break;

            case PlayerStates.Walk:
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
                RotatePlayerWithCamera = true;
                walk.Walk(waypoint, gameObject, groundCheck, playerModel, rb);
                animator.SetFloat("SpeedMultiplier", distanceFromWaypoint);
                customDamping.CustomDamping(rb);
                break;

            case PlayerStates.Run:
                // IF JUMP BUTTON HAS BEEN PRESSED, CHANGE STATE TO JUMP.
                if (jumpButtonPressed)
                {
                    //Debug.Log("JUMP BUTTON PRESSED ENTERED IN RUN CASE");
                    ChangeState(PlayerStates.Jump);
                    jumpButtonPressed = false;
                    return;
                }
                // IF ISRUNNING == FALSE, CHANGE STATE TO WALK.
                if (!isRunning)
                {
                    //Debug.Log("IS NOT RUNNING");
                    ChangeState(PlayerStates.Walk);
                    return;
                }
                // IF DISTANCE FROM THE WAYPOINT MOVED BY INPUTMANAGER.CS IS LESS THAN DEADZON VARIABLE, CHANGE STATE TO IDLE.
                if (distanceFromWaypoint <= deadZone)
                {
                    ChangeState(PlayerStates.Idle);
                    return;
                }
                RotatePlayerWithCamera = true;
                run.Run(waypoint, gameObject, groundCheck, playerModel, rb);
                customDamping.CustomDamping(rb);
                break;

            case PlayerStates.Jump:
                if (hitGround && isJumping == true)
                {
                    ChangeState(PlayerStates.Land);
                    return;
                }

                if (stateWhenJumpInput == PlayerStates.Walk)
                {
                    walk.Walk(waypoint, gameObject, groundCheck, playerModel, rb);
                }
                else
                {
                    run.Run(waypoint, gameObject, groundCheck, playerModel, rb);
                }
                customDamping.CustomDamping(rb);
                RotatePlayerWithCamera = true;

                // FASTER DOWNFORCE
                if (rb.linearVelocity.y < 0)
                {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * (1 + fasterDownwardforce), rb.linearVelocity.z);
                }

                // IF NOT JUMPING, JUMP
                if (!isJumping)
                {
                    jump.Jump(rb);
                    isJumping = true;
                    hitGround = false;
                }

                break;

            case PlayerStates.Land:
                ChangeState(PlayerStates.Idle);
                isJumping = false;
                jumpButtonPressed = false;
                walk.Walk(waypoint, gameObject, groundCheck, playerModel, rb);
                customDamping.CustomDamping(rb);
                RotatePlayerWithCamera = true;
                break;
        }
    }

    void ChangeState(PlayerStates state)
    {
        playerState = state;
        // TODO: CHANGE ALL THESE TO WORK WITH SENDING ROLLERBLADES BOOL AND SPLIT ROLLERBLADES INTO PLAYERANIMATIONCONTROLLER.
        //PlayerStateChange?.Invoke(state);
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (!context.started || !groundCheck.GroundedCheck(0.1f)) return;
        stateWhenJumpInput = playerState;
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

        if (rollerbladeCD > 0 && !customDamping.DampingEnabled) return;

        animator.SetBool("Rollerblade", !animator.GetBool("Rollerblade"));
        customDamping.DampingEnabled = !customDamping.DampingEnabled;
        rollerbladeCD = setRollerbladeCD;

        for (int i = 0; i < RegularBoots.Count; i++)
        {
            RegularBoots[i].SetActive(!customDamping.DampingEnabled);
        }
        Rollerblades.SetActive(customDamping.DampingEnabled);
    }
    public void HitGround(bool value)
    {
        hitGround = value;
    }
}
