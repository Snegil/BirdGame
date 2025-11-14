using System;
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

    void PlayerMovementAnimation(bool value, float speed)
    {
        animator.SetBool("IsMoving", value);
        if (value)
        {
            animator.speed = MathF.Abs(speed);
        }
        else
        {
            animator.speed = 1;
        }        
    }
}
