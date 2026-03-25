using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    PlayerMovementController playerMovementController;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float yOffset = 0.5f;

    [SerializeField]
    LayerMask interactMask;
    [SerializeField]
    float interactDistance = 0.5f;

    Transform playerModel;

    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerModel = animator.gameObject.transform;
    }

    public void InteractInput(InputAction.CallbackContext context)
    {
        if (!context.started || playerMovementController.IsJumping) return;

        animator.SetTrigger("Peck");
        Vector3 origin = new(playerModel.position.x, playerModel.position.y + yOffset, playerModel.position.z);
        Physics.Raycast(origin, playerModel.forward, out RaycastHit hit, interactDistance, interactMask);

        Debug.DrawRay(origin, playerModel.forward * interactDistance, Color.blue);

        if (hit.collider == null) return;

        if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactObject))
        {
            interactObject.Interact();
        }
    }
}
