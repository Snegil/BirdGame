using UnityEngine;

public class PlayerIdle : MonoBehaviour
{
    public void Idle(PlayerStates playerState, GroundCheck groundCheck, Animator animator)
    {
        animator.speed = 1.0f;

        if (playerState != PlayerStates.Idle || !groundCheck.GroundedCheck(0.1f))
        {
            return;
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.ResetTrigger("Movement");
            animator.SetTrigger("Idle");
        }
    }
}
