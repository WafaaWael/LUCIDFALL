using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent onInteracted;

    public void Interact()
    {
        onInteracted?.Invoke();
    }
}
public interface IInteractable
{
    void Interact();
}
