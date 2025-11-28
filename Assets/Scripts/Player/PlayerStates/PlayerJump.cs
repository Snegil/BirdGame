using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    float jumpPower = 10f;

    public void Jump(Rigidbody rb, GroundCheck groundCheck, Animator animator, PlayerController playerController)
    {
        animator.speed = 1;
        animator.SetTrigger("Jump");

        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
