using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    float jumpPower = 10f;

    Coroutine doJump;

    public void Jump(Rigidbody rb, GroundCheck groundCheck, Animator animator, PlayerController playerController)
    {
        if (doJump != null) return;

        doJump = StartCoroutine(DoJump(rb, groundCheck, animator, playerController));
    }
    //TODO: fix jump.
    IEnumerator DoJump(Rigidbody rb, GroundCheck groundCheck, Animator animator, PlayerController playerController)
    {
        animator.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        
        while (!groundCheck.GroundedCheck(0.2f))
        {
            yield return null;
        }

        animator.SetTrigger("Land");

        yield return new WaitForSeconds(0.2f);

        doJump = null;
        playerController.IsJumping(false);

        yield break;
    }
}
