using UnityEngine;

public class FloatNBob : MonoBehaviour
{
    [SerializeField]
    float sinOffset = 5;

    [SerializeField]
    float sinAmplification = 0.05f;

    [SerializeField]
    float sinFrequency = 10f;
    [SerializeField]
    float highestPoint = 0.5f;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(startPosition.x, Mathf.Abs(sinAmplification * Mathf.Sin(Time.time * sinFrequency + sinOffset)), startPosition.z);
        transform.Rotate(0.1f, 0.05f, 0.3f);
    }
}
