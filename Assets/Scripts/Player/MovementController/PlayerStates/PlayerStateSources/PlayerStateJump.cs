using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateJump", menuName = "PlayerStates/PlayerStateJump", order = 4)]
public class PlayerStateJump : ScriptableObject
{
    [SerializeField]
    float jumpPower = 10f;

    public void Jump(Rigidbody rb)
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
