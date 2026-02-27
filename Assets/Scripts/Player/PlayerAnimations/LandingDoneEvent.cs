using UnityEngine;

public class LandingDoneEvent : MonoBehaviour
{
    PlayerController playerController;

    void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }
    public void LandingDone()
    {

    }
}
