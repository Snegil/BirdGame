using UnityEngine;

public class CollectibleRotation : MonoBehaviour
{
    [SerializeField]
    Vector3 rotation = new(0, 0, 0.2f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation);
    }
}
