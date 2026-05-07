using UnityEngine;

public class HitGroundEvent : MonoBehaviour
{
    PlayerMovementController playerMovementController;

    void Awake()
    {
        playerMovementController = transform.parent.GetComponent<PlayerMovementController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!playerMovementController.IsJumping) return;
        Debug.Log("HIT GROUND");
        playerMovementController.HitGround(true);
    }
    void OnTriggerExit(Collider other)
    {
        playerMovementController.HitGround(false);
    }
}
