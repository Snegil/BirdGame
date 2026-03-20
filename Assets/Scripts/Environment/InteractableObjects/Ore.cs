using UnityEngine;

public class Ore : MonoBehaviour, IInteractable
{
    int oreAmount;

    public void Interact()
    {
        oreAmount--;
        Debug.Log("OreAmount: " + oreAmount);
    }
}
