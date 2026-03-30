using UnityEngine;
using UnityEngine.InputSystem;

public class QuickQuit : MonoBehaviour
{
    public void Quit(InputAction.CallbackContext context)
    {
        Debug.Log("QUITTING");
        Application.Quit();
    }
}
