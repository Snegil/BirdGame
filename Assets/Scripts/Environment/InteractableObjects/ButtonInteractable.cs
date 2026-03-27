using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    public List<IInteractAction> Actions()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        Debug.Log(this + " INTERACTED");
    }
}
