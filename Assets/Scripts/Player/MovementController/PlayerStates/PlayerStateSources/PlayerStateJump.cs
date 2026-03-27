using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateJump", menuName = "PlayerStates/PlayerStateJump", order = 2)]
public class PlayerStateJump : ScriptableObject
{
    [SerializeField]
    float jumpPower = 10f;

    public void Jump(Rigidbody rb)
    {
        rb.AddForce(rb.transform.up * jumpPower, ForceMode.Impulse);
    }
}
