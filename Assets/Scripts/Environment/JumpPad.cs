using System.Collections;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    float jumpPower;

    Collider collider;

    [SerializeField]
    float jumpPadCooldown = 2f;

    void Start()
    {
        collider = GetComponent<Collider>();
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
        collider.enabled = false;
        yield return new WaitForSeconds(jumpPadCooldown);
        collider.enabled = true;
        yield break;
    }
}
