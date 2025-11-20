using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField, Header("Length of raycast downwards.")]
    float distance = 1f;

    [SerializeField, Header("Which layer to hit.")]
    LayerMask layerMask;

    void Update() 
    {
        //Debug.DrawRay(transform.position, Vector3.down * distance, Color.green);
    }
    public bool GroundedCheck(float length = -20f)
    {
        if (length == -20f)
        {
            length = distance;
        }

        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, length, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
