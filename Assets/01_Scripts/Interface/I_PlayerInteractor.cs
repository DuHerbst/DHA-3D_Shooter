using UnityEngine;
using UnityEngine.InputSystem;

public class IPlayerInteractor : MonoBehaviour
{
    [SerializeField] private InputAction interactInput;
    
    private IInteractable _currentInteractable; // The current interactable that the player is looking at saved in memory

    void OnEnable()
    {
        
        interactInput.Enable();
        interactInput.performed += Interact;
        
    }

    void OnDisable()
    {
        interactInput.performed -= Interact;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(_currentInteractable != null) return;
        
        _currentInteractable = other.GetComponentInParent<IInteractable>();
        _currentInteractable?.OnHover();
        
    }

    private void OnTriggerExit(Collider other)
    {
        _currentInteractable?.OnHoverOut();
        _currentInteractable = null;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        
        _currentInteractable?.OnInteract(); // If the current interactable is not null, call the OnInteract method
        
    }
    
}
