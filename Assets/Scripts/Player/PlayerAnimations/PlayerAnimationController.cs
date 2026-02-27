using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField]
    Animator animator;

    [Space, SerializeField]
    float runningAnimatorSpeed = 2f;

    GroundCheck groundCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        groundCheck = GetComponent<GroundCheck>();
    }

    void OnEnable()
    {
        playerController.PlayerStateChange += AnimationUpdate;
    }
    void OnDisable()
    {
        playerController.PlayerStateChange -= AnimationUpdate;
    }

    void Update()
    {
        float distance = groundCheck.DistanceFromGround(Mathf.Infinity);
        animator.SetFloat("DistanceToGround", distance);
    }

    void AnimationUpdate(PlayerStates state)
    {
        switch (state)
        {
            case PlayerStates.Walk:
                animator.SetFloat("SpeedMultiplier", 1);
                animator.SetTrigger("Movement");
                break;
            case PlayerStates.Run:
                animator.SetFloat("SpeedMultiplier", runningAnimatorSpeed);
                animator.SetTrigger("Movement");
                break;
            case PlayerStates.Jump:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
                    return;
                }
                animator.SetFloat("SpeedMultiplier", 1);
                animator.SetTrigger("Jump");
                break;
            case PlayerStates.Land:
                animator.SetTrigger("Land");
                break;
            default:
                animator.SetFloat("SpeedMultiplier", 1);
                animator.SetTrigger("Idle");
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
