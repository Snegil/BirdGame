using UnityEngine;

public class ReturnToPlatform : MonoBehaviour
{
    Vector3 lastSafePosition;

    GroundCheck groundCheck;

    [Space, SerializeField]
    float timeUntilCheck;
    float setTimeUntilCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
        setTimeUntilCheck = timeUntilCheck;
    }
    void FixedUpdate()
    {
        if (transform.position.y < lastSafePosition.y - 80 && !groundCheck.GroundedCheck(Mathf.Infinity))
        {
            transform.position = lastSafePosition;
        }

        timeUntilCheck -= Time.deltaTime;

        if (timeUntilCheck > 0) return;

        if (groundCheck.GroundedCheck() && transform.position != lastSafePosition)
        {
            Vector3 oldLastSafePosition = lastSafePosition;
            lastSafePosition = transform.position;
            Debug.Log("Set safe position\n" + "Old: " + oldLastSafePosition + "\nNew: " + lastSafePosition);
        }

        timeUntilCheck = setTimeUntilCheck;
    }
}
