using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GroundCheck), typeof(Rigidbody), typeof(WinterTyre))]
public class PlayerController : MonoBehaviour
{
    public delegate void PlayerStateUpdate(PlayerStates playerState);
    public event PlayerStateUpdate PlayerStateChange;

    [Space, SerializeField]
    PlayerStates playerState;
    public bool AllowCamControl { get; set; } = false;

    [Space, SerializeField, Header("Deadzone distance from centre of player character:")]
    float deadZone;

    [Space, SerializeField]
    Transform waypoint;

    bool isjumping = false;

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

    WinterTyre winterTyre;

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

    void Start()
    {
        setRollerbladeCD = rollerbladeCD;
        rollerbladeCD = 0;

        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody>();
        winterTyre = GetComponent<WinterTyre>();
    }

    void FixedUpdate()
    {
        if (rollerbladeCD > 0) rollerbladeCD -= Time.deltaTime;

        Vector3 movementDirection = waypoint.position - gameObject.transform.position;
        movementDirection.y = 0; // Set the y component to 0 to prevent vertical movement
        movementDirection.Normalize(); // Normalize the direction vector

        float distanceFromWaypoint = Mathf.Clamp(Vector3.Distance(waypoint.position, transform.position), 0, 1);

        //Debug.Log(distanceFromWaypoint);

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
                }
                AllowCamControl = false;
                idle.Idle(groundCheck);

                break;

            case PlayerStates.Walk:
                // IF JUMP BUTTON HAS BEEN PRESSED, CHANGE STATE TO JUMP.
                if (jumpButtonPressed)
                {
                    //Debug.Log("JUMP BUTTON PRESSED ENTERED IN WALK CASE");
                    ChangeState(PlayerStates.Jump);
                    jumpButtonPressed = false;
                }
                // IF ISRUNNING == TRUE, CHANGE STATE TO RUN.
                if (isRunning)
                {
                    //Debug.Log("IS RUNNING");
                    ChangeState(PlayerStates.Run);
                }
                // IF DISTANCE FROM THE WAYPOINT MOVED BY INPUTMANAGER.CS IS LESS THAN DEADZON VARIABLE, CHANGE STATE TO IDLE.
                if (distanceFromWaypoint < deadZone)
                {
                    ChangeState(PlayerStates.Idle);
                }
                AllowCamControl = true;
                walk.Walk(waypoint, gameObject, groundCheck, playerModel, rb);
                winterTyre.CustomDamping(rb);
                break;

            case PlayerStates.Run:
                // IF JUMP BUTTON HAS BEEN PRESSED, CHANGE STATE TO JUMP.
                if (jumpButtonPressed)
                {
                    //Debug.Log("JUMP BUTTON PRESSED ENTERED IN RUN CASE");
                    ChangeState(PlayerStates.Jump);
                    jumpButtonPressed = false;
                }
                // IF ISRUNNING == FALSE, CHANGE STATE TO WALK.
                if (!isRunning)
                {
                    //Debug.Log("IS NOT RUNNING");
                    ChangeState(PlayerStates.Walk);
                }
                AllowCamControl = true;
                run.Run(waypoint, gameObject, groundCheck, playerModel, rb);
                winterTyre.CustomDamping(rb);
                break;

            case PlayerStates.Jump:
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6 && isjumping)
                {
                    ChangeState(PlayerStates.Land);
                    return;
                }

                walk.Walk(waypoint, gameObject, groundCheck, playerModel, rb);
                winterTyre.CustomDamping(rb);
                AllowCamControl = true;

                // FASTER DOWNFORCE
                if (rb.linearVelocity.y < 0)
                {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * (1 + fasterDownwardforce), rb.linearVelocity.z);
                }

                // IF NOT JUMPING, JUMP
                if (!isjumping)
                {
                    jump.Jump(rb);
                    isjumping = true;
                }

                break;

            case PlayerStates.Land:
                // IF ANIMATION CLIP IS FINISHED PLAYING, CHANGE STATE TO IDLE.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    ChangeState(PlayerStates.Idle);
                    isjumping = false;
                    jumpButtonPressed = false;
                    return;
                }

                walk.Walk(waypoint, gameObject, groundCheck, playerModel, rb);
                winterTyre.CustomDamping(rb);
                AllowCamControl = true;
                break;
        }
    }

    void ChangeState(PlayerStates state)
    {
        playerState = state;
        PlayerStateChange?.Invoke(state);
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (!context.started || !groundCheck.GroundedCheck(0.1f)) return;
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

        if (rollerbladeCD > 0 && !winterTyre.DampingEnabled) return;

        winterTyre.DampingEnabled = !winterTyre.DampingEnabled;
        rollerbladeCD = setRollerbladeCD;

        for (int i = 0; i < RegularBoots.Count; i++)
        {
            RegularBoots[i].SetActive(!winterTyre.DampingEnabled);
        }
        Rollerblades.SetActive(winterTyre.DampingEnabled);
    }
}
