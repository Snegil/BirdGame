using UnityEngine;

public class PlayerNormalRotate : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float lerpSpeed = 2;

    [SerializeField]
    LayerMask layerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Raycast down to the ground
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 20, layerMask))
        {
            //Rotate the player to the normal of the ground
            rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, Time.deltaTime * lerpSpeed);
        }
    }
}
