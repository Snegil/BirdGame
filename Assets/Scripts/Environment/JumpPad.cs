using System.Collections;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private static readonly int ActionHash = Animator.StringToHash("Action");

    [SerializeField]
    float jumpPower;

    Collider jumpCollider;

    [SerializeField]
    float jumpPadCooldown = 2f;

    Animator animator;

    void Start()
    {
        jumpCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() == null) return;

        Rigidbody collisionObjectRB = other.GetComponent<Rigidbody>();
        animator.SetTrigger(ActionHash);
        collisionObjectRB.AddForce(transform.up * jumpPower);
        StartCoroutine(CooldownRoutine());
    }

    IEnumerator CooldownRoutine()
    {
        jumpCollider.enabled = false;
        yield return new WaitForSeconds(jumpPadCooldown);
        jumpCollider.enabled = true;
        yield break;
    }
}
