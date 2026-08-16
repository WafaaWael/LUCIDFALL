using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;

    [Header("Interaction")]
    [SerializeField] private float interactionDistance = 3f;

    [Header("Events")]
    [SerializeField] private UnityEvent2 _onInteractionStarted;
    [SerializeField] private UnityEvent2 _onInteractionEnded;

    private InputSystem_Actions inputActions;

    private IInteractable currentInteractable;
    private IInteractable previousInteractable;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null &&
            inputActions.Player.Interact.WasPressedThisFrame())
        {
            currentInteractable.Interact();
        }
    }

    private void CheckForInteractable()
    {
        IInteractable detectedInteractable = null;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance))
        {
            detectedInteractable =
                hit.collider.GetComponentInParent<IInteractable>();
        }

        // Nothing detected
        if (detectedInteractable == null)
        {
            if (currentInteractable != null)
            {
                HideInteraction(currentInteractable);

                previousInteractable = currentInteractable;
                currentInteractable = null;

                _onInteractionEnded?.Invoke();
            }

            return;
        }

        // Already looking at the same object
        if (detectedInteractable == currentInteractable)
        {
            return;
        }

        // We were looking at another interactable
        if (currentInteractable != null)
        {
            HideInteraction(currentInteractable);
        }

        // New interactable detected
        currentInteractable = detectedInteractable;

        ShowInteraction(currentInteractable);

        _onInteractionStarted?.Invoke();
    }

    private void ShowInteraction(IInteractable interactable)
    {
        if (interactable._textImag == null)
            return;
        interactable.foundInteract();
        interactable._textImag.gameObject.SetActive(true);
        
    }

    private void HideInteraction(IInteractable interactable)
    {
        if (interactable._textImag == null)
            return;
        interactable.missInteract();
        interactable._textImag.gameObject.SetActive(false);

    }
}