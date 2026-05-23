using UnityEngine;

public class FasterDownForce : MonoBehaviour
{
    [SerializeField]
    float fasterDownwardforce = 0.05f;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // FASTER DOWNFORCE
        if (rb.linearVelocity.y < -1)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * (1 + fasterDownwardforce), rb.linearVelocity.z);
        }
    }
}
