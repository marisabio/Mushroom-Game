using UnityEngine;
using UnityEngine.Events;

public class InteractableController : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;

    public void Interact()
    {
        onInteract.Invoke();
    }
  
}