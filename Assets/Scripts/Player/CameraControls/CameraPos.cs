using System.Collections;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float lerpSpeed = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * lerpSpeed);
        //transform.position = player.position;
    }
}
