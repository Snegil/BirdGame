using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement playerMovement;

    Animator animator;

    void Awake()
    {
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        playerMovement.IsMoving += PlayerMovementAnimation;
    }
    void OnDisable()
    {
        playerMovement.IsMoving -= PlayerMovementAnimation;
    }

    void PlayerMovementAnimation(bool value, Vector3 speed)
    {
        animator.SetBool("IsMoving", value);
    }
}
