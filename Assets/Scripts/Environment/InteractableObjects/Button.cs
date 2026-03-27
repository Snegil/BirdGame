using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeReference]
    List<ButtonAction> actions = new();

    public void Interact()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Action();
        }
    }
}
