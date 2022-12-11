using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class InteractableEvent : EventData
{
    public Interactable interactable;
}

public class Interactable : MonoBehaviour
{
    public UnityEvent actions;
    public UnityEvent canInteract;
    public UnityEvent cantInteract;
    public void Interact()
    {
        Debug.Log("Interacted");
        actions?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            canInteract?.Invoke();
            EventSystem.GetInstance().EmitEvent("CanInteract", new InteractableEvent { interactable = this });
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            cantInteract?.Invoke();
            EventSystem.GetInstance().EmitEvent("CantInteract", new InteractableEvent { interactable = this });
        }
    }
}
