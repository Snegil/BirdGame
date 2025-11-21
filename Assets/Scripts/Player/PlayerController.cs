using Unity.VisualScripting;
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

    [Space, SerializeField, Header("Deadzone distance from centre of player character:")]
    float deadZone;

    [Space, SerializeField]
    Transform sphere;

    PlayerIdle playerIdle;
    PlayerRun playerRun;
    PlayerJump playerJump;

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
    }
    //TODO: DON'T LEAVE THIS HERE.
    bool isjumping = false;
    void FixedUpdate()
    {
        Vector3 movementDirection = sphere.position - gameObject.transform.position;
        movementDirection.y = 0; // Set the y component to 0 to prevent vertical movement
        movementDirection.Normalize(); // Normalize the direction vector
        Debug.Log(playerState);
        switch (playerState)
        {
            case PlayerStates.Idle:
                if (Mathf.Clamp(Vector3.Distance(gameObject.transform.position, sphere.position), 0, 1) > deadZone)
                {
                    playerState = PlayerStates.Run;
                    PlayerStateChange?.Invoke(playerState);
                    //playerIdle.ResetIdleSleepTimer(); // Reset the idle sleep timer when transitioning to run
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

                playerRun.Run(playerState, sphere, animator, groundCheck, playerModel, rb);
                winterTyre.CustomDamping(rb);
                break;

            case PlayerStates.Jump:
                if (!isjumping)
                {
                    isjumping = true;
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
        playerState = PlayerStates.Jump;
    }
}
