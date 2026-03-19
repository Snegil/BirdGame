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
        //Debug.Log("ENTERED TRIGGER");
        playerMovementController.HitGround(true);
    }
    void OnTriggerExit(Collider other)
    {
        playerMovementController.HitGround(false);
    }
}
