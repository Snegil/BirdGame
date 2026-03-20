using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [field: SerializeField]
    public PlayerMovementController PlayerMovementController { get; private set; }

    public bool IsJumping { get; set; } = false;

    [SerializeField]
    Animator animator;
}
