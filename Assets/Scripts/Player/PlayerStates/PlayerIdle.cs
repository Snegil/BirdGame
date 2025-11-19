using UnityEngine;

public class PlayerIdle : MonoBehaviour
{
    [SerializeField]
    float idleSleepTimer;
    float setIdleSleepTimer;

    void Awake()
    {
        setIdleSleepTimer = idleSleepTimer;
    }

    public void Idle(PlayerStates playerState, GroundCheck groundCheck, Animator animator) 
    {
        animator.speed = 1.0f;
        
        if (playerState != PlayerStates.Idle || !groundCheck.GroundedCheck())
        {
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.ResetTrigger("Movement");
            animator.SetTrigger("Idle");
        }        
        // if (!animator.GetCurrentAnimatorStateInfo(0).IsName("IdleSleep") && !animator.GetCurrentAnimatorStateInfo(0).IsName("IdleFollowUp") && !animator.GetCurrentAnimatorStateInfo(0).IsName("IdleWakingUp") && idleSleepTimer > 0)
        // {
        //     idleSleepTimer -= Time.deltaTime;
        //     return;
        // }

        // animator.SetTrigger("IdleSleep");
        // idleSleepTimer = setIdleSleepTimer;

    }
    //public void ResetIdleSleepTimer() { idleSleepTimer = setIdleSleepTimer; }
}
