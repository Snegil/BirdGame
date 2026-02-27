using UnityEngine;

public class HitGroundEvent : MonoBehaviour
{
    PlayerController playerController;

    void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ENTERED TRIGGER");
        //playerController.HitGround(true);
    }
    void OnTriggerExit(Collider other)
    {
        //playerController.HitGround(false);
    }
}
