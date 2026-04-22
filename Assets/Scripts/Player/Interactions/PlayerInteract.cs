using Unity.Mathematics;
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

    [SerializeField]
    Vector3 boxCastSize = new(0.1f, 0.1f, 0.1f);

    Transform playerModel;

    [SerializeField]
    Transform actionOrigin;

    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerModel = animator.gameObject.transform;
    }

    void Update()
    {
        DrawboxCustom.DrawBoxCastBox(actionOrigin.position, boxCastSize, playerModel.rotation, playerModel.forward, interactDistance, Color.green);
    }
    public void InteractInput(InputAction.CallbackContext context)
    {
        if (!context.started || playerMovementController.IsJumping) return;

        animator.SetTrigger("Peck");
        Vector3 origin = new(playerModel.position.x, playerModel.position.y + yOffset, playerModel.position.z);
        //Physics.Raycast(origin, playerModel.forward, out RaycastHit hit, interactDistance, interactMask);
        Physics.BoxCast(actionOrigin.position, boxCastSize, playerModel.forward, out RaycastHit hit, playerModel.rotation, interactDistance, interactMask);

        //Debug.DrawRay(origin, playerModel.forward * interactDistance, Color.blue);

        if (hit.collider == null) return;

        if (hit.collider.TryGetComponent(out IInteractable interactObject))
        {
            interactObject.Interact();
        }
    }
}
