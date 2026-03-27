using UnityEngine;

public class ButtonAction : MonoBehaviour, IInteractAction
{
    public virtual void Action()
    {
        Debug.Log("Action");
    }
}
