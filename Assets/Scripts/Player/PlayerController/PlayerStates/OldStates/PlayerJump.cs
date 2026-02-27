using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    float jumpPower = 10f;

    public void Jump(Rigidbody rb)
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
