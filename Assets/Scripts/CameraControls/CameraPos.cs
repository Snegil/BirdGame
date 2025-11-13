using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField]
    Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}
