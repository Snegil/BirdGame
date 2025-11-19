using UnityEngine;

public class PlayerNormalRotate : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Transform playerModel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Raycast down to the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100, layerMask))
        {
            //Rotate the player to the normal of the ground
            rb.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }       
    }
}
