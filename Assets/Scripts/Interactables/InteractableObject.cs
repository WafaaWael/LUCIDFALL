using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject textImage;
    [SerializeField] private string interactionText = "Press E to interact";
    [SerializeField] private TextMeshProUGUI interactionTextUI;
    [SerializeField] private UnityEvent onInteracted;
    [SerializeField] private GameObject _uIParent;
    GameObject IInteractable._textImag => textImage;
    GameObject IInteractable._uIParent => _uIParent;
    public void Interact()
    {
        onInteracted?.Invoke();
    }
    public void SetUIParentActive(bool isActive)
    {
        if (_uIParent != null)
        {
            _uIParent.SetActive(isActive);
        }
    }
    public void foundInteract()
    {
        SetUIParentActive(true);
        interactionTextUI.gameObject.SetActive(true);
        interactionTextUI.text = interactionText;
        interactionTextUI.gameObject.SetActive(true);
    }
    public void missInteract()
    {
               SetUIParentActive(false);
        interactionTextUI.gameObject.SetActive(false);
    }
}
public interface IInteractable
{
    GameObject _textImag { get; }
    GameObject _uIParent { get; }
    void Interact();
    void foundInteract();
    void missInteract();
}
