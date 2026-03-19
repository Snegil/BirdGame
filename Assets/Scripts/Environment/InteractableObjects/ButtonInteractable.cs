using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log(this + " INTERACTED");
    }
}
