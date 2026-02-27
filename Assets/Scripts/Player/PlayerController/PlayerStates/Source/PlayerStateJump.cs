using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateJump", menuName = "ScriptableObjects/PlayerStateJump", order = 3)]
public class PlayerStateJump : ScriptableObject
{
    [SerializeField]
    float jumpPower = 10f;

    public void Jump(Rigidbody rb)
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
