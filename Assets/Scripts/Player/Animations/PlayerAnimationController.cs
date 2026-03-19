using System.ComponentModel.Design;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerMovementController playerMovementController;

    [SerializeField]
    Animator animator;

    [Space, SerializeField]
    float runningAnimatorSpeed = 2f;

    GroundCheck groundCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        groundCheck = GetComponent<GroundCheck>();
    }

    void OnEnable()
    {
        playerMovementController.PlayerStateChange += AnimationUpdate;
    }
    void OnDisable()
    {
        playerMovementController.PlayerStateChange -= AnimationUpdate;
    }

    void Update()
    {
        float distance = groundCheck.DistanceFromGround(Mathf.Infinity);
        animator.SetFloat("DistanceToGround", distance);
    }

    void AnimationUpdate(PlayerStates state, bool rollerblades)
    {
        animator.SetBool("Rollerblade", rollerblades);
        switch (state)
        {
            case PlayerStates.Idle:
                ResetAllTriggers();
                animator.SetFloat("SpeedMultiplier", 1);
                animator.SetTrigger("Idle");
                break;
            case PlayerStates.Walk:
                ResetAllTriggers();
                animator.SetFloat("SpeedMultiplier", 1);
                animator.SetTrigger("Movement");
                break;
            case PlayerStates.Run:
                ResetAllTriggers();
                animator.SetFloat("SpeedMultiplier", runningAnimatorSpeed);
                animator.SetTrigger("Movement");
                break;
            case PlayerStates.Jump:
                ResetAllTriggers();
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
                    return;
                }
                animator.SetFloat("SpeedMultiplier", 1);
                animator.SetTrigger("Jump");
                break;
            case PlayerStates.Land:
                ResetAllTriggers();
                animator.SetTrigger("Land");
                break;
            default:
                break;
        }

    }
    void ResetAllTriggers()
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(param.name);
            }
        }
    }
}
