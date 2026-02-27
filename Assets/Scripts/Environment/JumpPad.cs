using System.Collections;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    float jumpPower;

    Collider jumpCollider;

    [SerializeField]
    float jumpPadCooldown = 2f;

    void Start()
    {
        jumpCollider = GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() == null) return;

        Rigidbody collisionObjectRB = other.GetComponent<Rigidbody>();

        collisionObjectRB.linearVelocity = new Vector3(0, jumpPower, 0);
        Debug.Log(collisionObjectRB.linearVelocity);
        StartCoroutine(CooldownRoutine());
    }

    IEnumerator CooldownRoutine()
    {
        jumpCollider.isTrigger = false;
        yield return new WaitForSeconds(jumpPadCooldown);
        jumpCollider.isTrigger = true;
        yield break;
    }
}
