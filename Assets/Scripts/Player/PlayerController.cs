using System.Collections.Generic;
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
    public bool AllowCamControl { get; set; } = false;

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
    CapsuleCollider playerCollider;

    [SerializeField]
    float rollerbladeCD = 10f;
    float setRollerbladeCD;

    [SerializeField]
    List<GameObject> RegularBoots = new();
    [SerializeField]
    GameObject Rollerblades;

    [SerializeField]
    float fasterDownwardforce = 0.2f;

    void Start()
    {
        setRollerbladeCD = rollerbladeCD;
        rollerbladeCD = 0;

        playerIdle = GetComponent<PlayerIdle>();
        playerRun = GetComponent<PlayerRun>();
        playerJump = GetComponent<PlayerJump>();
        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody>();
        winterTyre = GetComponent<WinterTyre>();
        playerCollider = GetComponent<CapsuleCollider>();

        animator = playerModel.GetComponent<Animator>();
        animator.Update(0f);
        animator.Update(0f);
    }
    void FixedUpdate()
    {
        if (rollerbladeCD > 0) rollerbladeCD -= Time.deltaTime;

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
                AllowCamControl = false;
                playerIdle.Idle(playerState, groundCheck, animator);
                break;

            case PlayerStates.Run:
                if (Mathf.Clamp(Vector3.Distance(gameObject.transform.position, sphere.position), 0, 1) < deadZone)
                {
                    playerState = PlayerStates.Idle;
                    PlayerStateChange?.Invoke(playerState);
                    return;
                }
                AllowCamControl = true;
                //Last boolean in playerrun.run() is to run animation!
                playerRun.Run(playerState, sphere, animator, groundCheck, playerModel, rb, winterTyre.RollerBladeOn, true);
                winterTyre.CustomDamping(rb);
                break;

            case PlayerStates.Jump:
                AllowCamControl = true;
                playerRun.Run(playerState, sphere, animator, groundCheck, playerModel, rb, winterTyre.RollerBladeOn, false);
                winterTyre.CustomDamping(rb);

                if (isjumping && groundCheck.GroundedCheck(0.1f))
                {
                    IsJumping(false);
                    playerState = PlayerStates.Idle;
                    return;
                }

                if (isjumping && !groundCheck.GroundedCheck(0.1f) && rb.linearVelocity.y < 0)
                {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * (1 + fasterDownwardforce), rb.linearVelocity.z);
                }

                if (isjumping && !groundCheck.GroundedCheck(0.1f)) return;

                IsJumping(true);
                playerJump.Jump(rb, groundCheck, animator, this);

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
    public void ToggleRollerblades(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (rollerbladeCD > 0 && !winterTyre.RollerBladeOn) return;

        winterTyre.RollerBladeOn = !winterTyre.RollerBladeOn;
        rollerbladeCD = setRollerbladeCD;

        for (int i = 0; i < RegularBoots.Count; i++)
        {
            RegularBoots[i].SetActive(!winterTyre.RollerBladeOn);
        }
        Rollerblades.SetActive(winterTyre.RollerBladeOn);

        animator.SetBool("Rollerblade", winterTyre.RollerBladeOn);
    }
}
