using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerIdle), typeof(PlayerRun), typeof(PlayerJump))]
[RequireComponent(typeof(GroundCheck), typeof(Rigidbody), typeof(WinterTyre))]
public class PlayerController : MonoBehaviour
{
    public delegate void PlayerStateUpdate(PlayerStates playerState);
    public event PlayerStateUpdate PlayerStateChange;

    [Space, SerializeField]
    PlayerStates playerState;
    public PlayerStates PlayerState { get { return playerState; } }
    PlayerStates prevState;

    [Space, SerializeField, Header("Deadzone distance from centre of player character:")]
    float deadZone;

    [Space, SerializeField]
    Transform sphere;

    PlayerIdle playerIdle;
    PlayerRun playerRun;
    PlayerJump playerJump;

    bool isjumping = false;

    WinterTyre winterTyre;

    GroundCheck groundCheck;
    Rigidbody rb;
    
    [SerializeField]
    GameObject playerModel;    
    Animator animator;


    void Start()
    {
        playerIdle = GetComponent<PlayerIdle>();
        playerRun = GetComponent<PlayerRun>();
        playerJump = GetComponent<PlayerJump>();
        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody>();
        winterTyre = GetComponent<WinterTyre>();

        animator = playerModel.GetComponent<Animator>();
        animator.Update(0f);
        animator.Update(0f);
    }
    void FixedUpdate()
    {
        Vector3 movementDirection = sphere.position - gameObject.transform.position;
        movementDirection.y = 0; // Set the y component to 0 to prevent vertical movement
        movementDirection.Normalize(); // Normalize the direction vector

        switch (playerState)
        {
            case PlayerStates.Idle:
                if (Mathf.Clamp(Vector3.Distance(gameObject.transform.position, sphere.position), 0, 1) > deadZone)
                {
                    playerState = PlayerStates.Run;
                    PlayerStateChange?.Invoke(playerState);
                    return;
                }
                playerIdle.Idle(playerState, groundCheck, animator);
                break;

            case PlayerStates.Run:
                if (Mathf.Clamp(Vector3.Distance(gameObject.transform.position, sphere.position), 0, 1) < deadZone)
                {
                    playerState = PlayerStates.Idle;
                    PlayerStateChange?.Invoke(playerState);
                    return;
                }
                //Last boolean in playerrun.run() is to run animation!
                playerRun.Run(playerState, sphere, animator, groundCheck, playerModel, rb, true);
                winterTyre.CustomDamping(rb);
                break;

            case PlayerStates.Jump:
                playerRun.Run(playerState, sphere, animator, groundCheck, playerModel, rb, false);
                winterTyre.CustomDamping(rb);

                if (isjumping && groundCheck.GroundedCheck(0.1f))
                {
                    IsJumping(false);
                    playerState = prevState;
                    playerJump.Land(animator, this);
                    return;
                }
                
                if (isjumping && !groundCheck.GroundedCheck(0.1f)) return;

                if (!isjumping)
                {
                    IsJumping(true);
                    playerJump.Jump(rb, groundCheck, animator, this);
                }
                
                break;
        }
    }
    public void IsJumping(bool value)
    {
        isjumping = value;
    }
    public void JumpInput(InputAction.CallbackContext context)
    {
        if (!context.started || !groundCheck.GroundedCheck(0.1f)) { return; }
        if (prevState != PlayerStates.Jump) prevState = playerState;
        playerState = PlayerStates.Jump;
    }
}
