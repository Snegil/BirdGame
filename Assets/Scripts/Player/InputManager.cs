using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    Transform sphere;

    [SerializeField]
    PlayerController playerController;

    bool hasInput = false;

    Vector3 readValue;

    void Start()
    {
        //playerController = GetComponent<PlayerController>();

        //THIS IS ONLY FOR THE RARE CASE THAT IT IS NOT ZEROED ONTO THE PLAYER
        sphere.localPosition = Vector3.zero;
    }
    void Update()
    {
        if (hasInput)
        {
            sphere.localPosition = new Vector3(readValue.x, 0, readValue.z);    
        }    
    }

    public void Input(InputAction.CallbackContext context)
    {
        readValue = context.ReadValue<Vector3>();
        if (context.started)
        {
            hasInput = true;
        }
        
    }
}
